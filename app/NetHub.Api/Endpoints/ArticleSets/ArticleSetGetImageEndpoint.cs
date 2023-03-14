using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Resources;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;

namespace NetHub.Api.Endpoints.ArticleSets;

[AllowAnonymous]
[Tags(TagNames.ArticleSets)]
[ApiVersion(Versions.V1)]
public class ArticleSetGetImageEndpoint : Endpoint<long, string[]>
{
    [HttpGet("articles/{id:long}/images"), ClientSide(ActionName = "getImages")]
    public override async Task<string[]> HandleAsync(long id, CancellationToken ct)
    {
        var articleSet = await Database.Set<ArticleSet>()
            .Include(as_ => as_.Images)
            .SingleOrDefaultAsync(as_ => as_.Id == id, ct);

        if (articleSet is null)
            throw new NotFoundException($"No such article set with id: {id}");

        var resources = articleSet.Images?
            .Select(i => Request.GetResourceUrl(i.ResourceId))
            .ToArray();

        return resources ?? Array.Empty<string>();
    }
}