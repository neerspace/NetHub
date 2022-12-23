using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Views;

namespace NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;

public class GetSavedArticlesHandler : AuthorizedHandler<GetSavedArticlesRequest, ExtendedArticleModel[]>
{
	public GetSavedArticlesHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<ExtendedArticleModel[]> Handle(GetSavedArticlesRequest request)
	{
		var userId = UserProvider.GetUserId();

		var saved = await Database.Set<ExtendedUserArticle>()
			.Where(ea => ea.UserId == userId
			             && ea.IsSaved == true
				//TODO: Remove comments in release (please...)
				// && ea.Status == ContentStatus.Published
			)
			.ProjectToType<ExtendedArticleModel>()
			.ToArrayAsync();

		return saved.DistinctBy(s => s.LocalizationId).ToArray();
	}
}