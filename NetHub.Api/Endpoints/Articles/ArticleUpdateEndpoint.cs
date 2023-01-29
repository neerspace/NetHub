using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Articles;

namespace NetHub.Api.Endpoints.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleUpdateEndpoint : ActionEndpoint<UpdateArticleRequest>
{
    [HttpPut("articles")]
    public override async Task HandleAsync([FromBody] UpdateArticleRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id, ct);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        if (request.AuthorId is not null)
            article.AuthorId =
                await Database.Set<AppUser>().FirstOrDefaultAsync(p => p.Id == request.AuthorId, ct) is null
                    ? throw new NotFoundException("No user with such Id")
                    : request.AuthorId.Value;

        if (request.Name is not null)
            article.Name = request.Name;

        article.Updated = DateTimeOffset.UtcNow;
        article.OriginalArticleLink = request.OriginalArticleLink;

        await Database.SaveChangesAsync(ct);
    }
}