using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.ArticleSets;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.ArticleSets;

[Authorize]
[Tags(TagNames.ArticleSets)]
[ApiVersion(Versions.V1)]
public sealed class ArticleSetUpdateEndpoint : ActionEndpoint<ArticleSetUpdateRequest>
{
    [HttpPut("articles")]
    public override async Task HandleAsync([FromBody] ArticleSetUpdateRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var articleSet = await Database.Set<ArticleSet>().FirstOr404Async(a => a.Id == request.Id, ct);

        if (articleSet.AuthorId != userId)
            throw new PermissionsException();

        if (request.AuthorId is not null)
            articleSet.AuthorId =
                await Database.Set<AppUser>().FirstOrDefaultAsync(p => p.Id == request.AuthorId, ct) is null
                    ? throw new NotFoundException("No user with such Id")
                    : request.AuthorId.Value;

        articleSet.OriginalArticleLink = request.OriginalArticleLink;

        await Database.SaveChangesAsync(ct);
    }
}