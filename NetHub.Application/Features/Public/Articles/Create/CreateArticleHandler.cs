using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Articles.Create;

internal sealed class CreateArticleHandler : AuthorizedHandler<CreateArticleRequest, ArticleModel>
{
    public CreateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<ArticleModel> Handle(CreateArticleRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUser();

        var articleEntity = new Article
        {
            AuthorId = user.Id, Name = request.Name, Created = DateTime.UtcNow,
            OriginalArticleLink = request.OriginalArticleLink,
        };

        var createdEntity = await Database.Set<Article>().AddAsync(articleEntity, ct);

        await Database.SaveChangesAsync(ct);

        if (request.Tags is not null)
        {
            foreach (var tag in request.Tags)
            {
                var existedTag = await Database.Set<Tag>().FirstOrDefaultAsync(t => t.Name == tag, ct);
                var tagId = existedTag?.Id;

                if (existedTag is null)
                {
                    var dbTag = Database.Set<Tag>().Add(new Tag { Name = tag.ToLower() });
                    await Database.SaveChangesAsync(ct);
                    tagId = dbTag.Entity.Id;
                }

                Database.Set<ArticleTag>().Add(new ArticleTag
                {
                    TagId = tagId!.Value,
                    ArticleId = createdEntity.Entity.Id
                });
            }
        }


        await Database.SaveChangesAsync(ct);

        return createdEntity.Entity.Adapt<ArticleModel>();
    }
}