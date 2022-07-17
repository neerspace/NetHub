using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Extensions;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Localizations.Html;

public class AddLocalizationHtmlHandler : AuthorizedHandler<AddLocalizationHtmlRequest>
{
	public AddLocalizationHtmlHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(AddLocalizationHtmlRequest request)
	{
		var userId = UserProvider.GetUserId();

		var localization = await Database.Set<ArticleLocalization>()
			.Include(al => al.Contributors)
			.FirstOr404Async(al =>
				al.ArticleId == request.ArticleId && al.LanguageCode == request.LanguageCode);

		if (localization.GetAuthorId() != userId)
			throw new PermissionsException();

		if (localization.InternalStatus == InternalStatus.AddedHtml)
			throw new ValidationFailedException("Html for this article already added");

		localization.Html = request.Html;
		localization.InternalStatus = InternalStatus.AddedHtml;

		await Database.SaveChangesAsync();

		return Unit.Value;
	}
}