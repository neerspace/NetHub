using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Core.Constants;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Localizations.Create;

public class CreateArticleLocalizationHandler :
	AuthorizedHandler<CreateArticleLocalizationRequest, ArticleLocalizationModel>
{
	public CreateArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<ArticleLocalizationModel> Handle(CreateArticleLocalizationRequest request)
	{
		var userId = UserProvider.GetUserId();
		var article = await Database.Set<Article>()
			.Include(a => a.Localizations)
			.FirstOr404Async(a => a.Id == request.ArticleId);

		if (article.Localizations?.FirstOrDefault(l => l.LanguageCode == ProjectConstants.UA) is null &&
		    request.LanguageCode != ProjectConstants.UA)
			throw new ApiException("First article must be ukrainian");

		if (article.Localizations?.FirstOrDefault(l => l.LanguageCode == request.LanguageCode) is not null)
			throw new ValidationFailedException("LanguageCode",
				"Article Localization with such language already exists");

		if (await Database.Set<Language>().FirstOrDefaultAsync(l => l.Code == request.LanguageCode) is null)
			throw new ValidationFailedException("LanguageCode", "No such language registered");

		var localization = request.Adapt<ArticleLocalization>();

		localization.Contributors = (await SetContributors(request.Contributors, userId)).ToArray();
		localization.Status = ContentStatus.Draft;
		localization.InternalStatus = InternalStatus.Created;

		var createdEntity = await Database.Set<ArticleLocalization>().AddAsync(localization);

		// await HtmlTools.CheckLinks(Database, request.ArticleId, request.Html);

		await Database.SaveChangesAsync();

		return createdEntity.Entity.Adapt<ArticleLocalizationModel>();
	}

	private async Task<IEnumerable<ArticleContributor>> SetContributors(ArticleContributorModel[]? contributors,
		long mainAuthorId)
	{
		var returnContributors = new List<ArticleContributor>
		{
			new()
			{
				UserId = mainAuthorId,
				Role = ArticleContributorRole.Author
			}
		};
		if (contributors is not {Length: > 0}) return returnContributors;

		if (contributors.FirstOrDefault(a => a.Role == ArticleContributorRole.Author) is not null)
			throw new ApiException("You can not set authors");

		foreach (var contributor in contributors)
		{
			var count = contributors.Count(a => a.UserId == contributor.UserId && a.Role == contributor.Role);
			if (count > 1)
				throw new ApiException("One user can not contribute the same role several times");

			var dbContributor = await Database.Set<Data.SqlServer.Entities.User>()
				.FirstOrDefaultAsync(p => p.Id == contributor.UserId);
			if (dbContributor is null)
				throw new NotFoundException($"No user with id: {contributor.UserId}");

			returnContributors.Add(contributor.Adapt<ArticleContributor>());
		}

		return returnContributors;
	}
}