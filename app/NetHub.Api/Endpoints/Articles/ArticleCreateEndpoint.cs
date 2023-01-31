using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Articles;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleCreateEndpoint : Endpoint<CreateArticleRequest, ArticleModel>
{
    [HttpPost("articles")]
    public override async Task<ArticleModel> HandleAsync([FromBody] CreateArticleRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        var articleEntity = new Article
        {
            AuthorId = user.Id,
            Name = request.Name,
            Created = DateTimeOffset.UtcNow,
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