using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Extensions;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Localizations.Status.Publish;

public class SetArticleStatusHandler : AuthorizedHandler<SetArticleStatusRequest>
{
	public SetArticleStatusHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(SetArticleStatusRequest statusRequest)
	{
		var userId = UserProvider.GetUserId();

		var localization = await Database.Set<ArticleLocalization>()
			.Include(al => al.Contributors)
			.FirstOr404Async(al => al.ArticleId == statusRequest.Id && al.LanguageCode == statusRequest.LanguageCode);

		CheckStatuses(statusRequest, localization, userId);

		await Database.SaveChangesAsync();

		return Unit.Value;
	}

	private static void CheckStatuses(SetArticleStatusRequest request, ArticleLocalization localization, long userId)
	{
		if (request.Status == ArticleStatusRequest.Publish)
		{
			if (localization.Status == ContentStatus.Published)
				return;
			
			if (localization.GetAuthorId() != userId ||
			    localization.Status is not
				    (ContentStatus.Draft or ContentStatus.Pending))
				throw new PermissionsException();
			localization.Status = ContentStatus.Pending;
		}

		if (request.Status == ArticleStatusRequest.UnPublish)
		{
			if (localization.GetAuthorId() != userId ||
			    localization.Status is not
				    (ContentStatus.Draft or ContentStatus.Published or ContentStatus.Pending))
				throw new PermissionsException();
			localization.Status = ContentStatus.Draft;
		}
	}
}