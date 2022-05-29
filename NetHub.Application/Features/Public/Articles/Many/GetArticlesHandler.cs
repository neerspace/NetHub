using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Articles.Many;

public class GetArticlesHandler : AuthorizedHandler<GetArticlesRequest, ArticleModel[]>
{
	public GetArticlesHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	public override async Task<ArticleModel[]> Handle(GetArticlesRequest request, CancellationToken cancel)
	{
		var userId = UserProvider.GetUserId();

		var articles = await Database.Set<Article>()
			.Where(a => a.AuthorId == userId)
			.ProjectToType<ArticleModel>()
			.ToArrayAsync(cancel);

		return articles;
	}
}