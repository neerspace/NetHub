using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.ArticleSets;

[Authorize]
[Tags(TagNames.ArticleSets)]
[ApiVersion(Versions.V1)]
public sealed class ArticleSetDeleteEndpoint : ActionEndpoint<long>
{
    [HttpDelete("articles/{id:long}")]
    public override async Task HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        var articleSet = await Database.Set<ArticleSet>().FirstOr404Async(a => a.Id == id, ct);

        if (articleSet.AuthorId != userId)
            throw new PermissionsException();

        Database.Set<ArticleSet>().Remove(articleSet);
        await Database.SaveChangesAsync(ct);
    }
}