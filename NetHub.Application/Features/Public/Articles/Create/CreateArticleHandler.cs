using Mapster;
using NetHub.Application.Extensions;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Create;

public class CreateArticleHandler : AuthorizedHandler<CreateArticleRequest, ArticleModel>
{
    public CreateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<ArticleModel> Handle(CreateArticleRequest request)
    {
        var user = UserProvider.GetUser();

        var articleEntity = new Article
        {
            AuthorId = user.Id,
            AuthorName = user.UserName,
            Status = ContentStatus.Draft
        };

        var createdEntity = await Database.Set<Article>().AddAsync(articleEntity);

        await Database.SaveChangesAsync();

        return createdEntity.Entity.Adapt<ArticleModel>();
    }
}