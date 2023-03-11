using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.ArticleSets;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.ArticleSets;

[Authorize]
[Tags(TagNames.ArticleSets)]
[ApiVersion(Versions.V1)]
public sealed class ArticleSetCreateEndpoint : Endpoint<ArticleSetCreateRequest, ArticleSetModelExtended>
{
    [HttpPost("articles")]
    public override async Task<ArticleSetModelExtended> HandleAsync([FromBody] ArticleSetCreateRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        var articleSetEntity = new ArticleSet
        {
            AuthorId = user.Id,
            Created = DateTimeOffset.UtcNow,
            OriginalArticleLink = request.OriginalArticleLink,
        };

        var createdEntity = await Database.Set<ArticleSet>().AddAsync(articleSetEntity, ct);

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

                Database.Set<ArticleSetTag>().Add(new ArticleSetTag
                {
                    TagId = tagId!.Value,
                    ArticleSetId = createdEntity.Entity.Id
                });
            }
        }

        await Database.SaveChangesAsync(ct);

        return createdEntity.Entity.Adapt<ArticleSetModelExtended>();
    }
}