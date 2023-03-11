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
            .Include(a => a.Articles!
                .Where(l => l.Status == ContentStatus.Published))
            .ThenInclude(l => l.Contributors).ThenInclude(c => c.User)
            .Include(a => a.Tags)!.ThenInclude(at => at.Tag)
            .Include(a => a.Images)
            .FirstOr404Async(a => a.Id == id, ct);

        GuardPermissions(articleSet, username);

        var model = articleSet.Adapt<ArticleSetModelExtended>();
        var imageIds = articleSet.Images?.Select(i => i.ResourceId).ToArray();

        if (imageIds != null && imageIds.Any())
            model.ImagesLinks = imageIds.Select(guid => Request.GetResourceUrl(guid)).ToArray();

        return model;
    }

    private static void GuardPermissions(ArticleSet articleSet, string? userName)
    {
        if (articleSet.Articles!.Any(l => l.Status == ContentStatus.Published))
            return;

        if (!articleSet.Articles!.Any())
            return;

        if (userName is null ||
            !articleSet.Articles!
                .Any(l => l.Contributors
                    .Select(c => c.User!.NormalizedUserName)
                    .Contains(userName, StringComparer.OrdinalIgnoreCase)))
            throw new PermissionsException();
    }
}