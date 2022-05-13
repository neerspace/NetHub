using Mapster;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.One;

public class GetArticleHandler : DbHandler<GetArticleRequest, ArticleModel>
{
    public GetArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<ArticleModel> Handle(GetArticleRequest request)
    {
        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id);

        return article.Adapt<ArticleModel>();
    }
}