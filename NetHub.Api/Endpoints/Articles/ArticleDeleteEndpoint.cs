using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Api.Endpoints.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleDeleteEndpoint : ActionEndpoint<long>
{
    [HttpDelete("articles/{id:long}")]
    public override async Task HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == id, ct);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        Database.Set<Article>().Remove(article);
        await Database.SaveChangesAsync(ct);
    }
}