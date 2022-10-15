using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Localizations.ToggleSaving;

public class ToggleArticleSaveHandler : AuthorizedHandler<ToggleArticleSaveRequest>
{
	public ToggleArticleSaveHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(ToggleArticleSaveRequest request)
	{
		var userId = UserProvider.GetUserId();

		var savedArticleEntity = await Database.Set<SavedArticle>()
			.Include(sa => sa.Localization)
			.Where(sa => sa.Localization != null &&
			             sa.Localization.ArticleId == request.ArticleId &&
			             sa.Localization.LanguageCode == request.LanguageCode)
			.FirstOrDefaultAsync();

		if (savedArticleEntity is null)
		{
			var localization = await Database.Set<ArticleLocalization>()
				.Where(al => al.ArticleId == request.ArticleId &&
				             al.LanguageCode == request.LanguageCode)
				.FirstOr404Async();

			await Database.Set<SavedArticle>().AddAsync(new SavedArticle
			{
				UserId = userId,
				LocalizationId = localization.Id,
			});

			await Database.SaveChangesAsync();

			return Unit.Value;
		}

		Database.Set<SavedArticle>().Remove(savedArticleEntity);
		await Database.SaveChangesAsync();

		return Unit.Value;
	}
}