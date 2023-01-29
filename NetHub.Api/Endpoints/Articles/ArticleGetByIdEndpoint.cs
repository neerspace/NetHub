using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Models.Articles;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Api.Endpoints.Articles;

[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleGetByIdEndpoint : Endpoint<long, (ArticleModel, Guid[]?)>
{
    [HttpGet("articles/{id:long}")]
    public override async Task<(ArticleModel, Guid[]?)> HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var article = await Database.Set<Article>()
            .Include(a => a.Localizations!.Where(l => l.Status == ContentStatus.Published))
            .Include(a => a.Tags)!.ThenInclude(at => at.Tag)
            .Include(a => a.Images)
            .FirstOr404Async(a => a.Id == id, ct);

        var model = article.Adapt<ArticleModel>();
        var imageIds = article.Images?.Select(i => i.ResourceId).ToArray();

        return (model, imageIds);
    }
}