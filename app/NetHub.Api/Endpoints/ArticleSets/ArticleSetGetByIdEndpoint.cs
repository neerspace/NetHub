using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.ArticleSets;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;

namespace NetHub.Api.Endpoints.ArticleSets;

[Authorize]
[Tags(TagNames.ArticleSets)]
[ApiVersion(Versions.V1)]
public sealed class ArticleSetGetByIdEndpoint : Endpoint<long, ArticleSetModelExtended>
{
    [HttpGet("articles/{id:long}"), ClientSide(ActionName = "getById")]
    public override async Task<ArticleSetModelExtended> HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var username = UserProvider.UserName;

        var articleSet = await Database.Set<ArticleSet>()
            .Include(a => a.Articles!)
            .ThenInclude(l => l.Contributors).ThenInclude(c => c.User)
            .Include(a => a.Tags)!.ThenInclude(at => at.Tag)
            .Include(a => a.Images)
            .FirstOr404Async(a => a.Id == id, ct);

        GuardPermissions(articleSet, username);

        var model = articleSet.Adapt<ArticleSetModelExtended>();
        var imageIds = articleSet.Images?.Select(i => i.ResourceId).ToArray();

        model.ImagesLinks = imageIds is {Length: > 0}
            ? imageIds.Select(guid => Request.GetResourceUrl(guid)).ToArray()
            : Array.Empty<string>();

        return model;
    }

    private static void GuardPermissions(ArticleSet articleSet, string? userName)
    {
        bool isContributor = articleSet.Articles!
            .Any(l => l.Contributors
                .Select(c => c.User!.NormalizedUserName)
                .Contains(userName, StringComparer.OrdinalIgnoreCase));

        if (articleSet.Articles!.Any(l => l.Status == ContentStatus.Published))
        {
            if (!isContributor)
                articleSet.Articles = articleSet.Articles!
                    .Where(as_ => as_.Status == ContentStatus.Published)
                    .ToArray();

            return;
        }

        if (!articleSet.Articles!.Any())
            return;

        if (userName is null ||
            !isContributor)
            throw new PermissionsException();
    }
}