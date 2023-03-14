using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Articles;

namespace NetHub.Admin.Api.Endpoints.Articles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Localizations)]
[Authorize(Policy = Policies.HasManageArticlesPermission)]
public class ArticleUpdateEndpoint : ActionEndpoint<ArticleModel>
{
    private readonly ISqlServerDatabase _database;

    public ArticleUpdateEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpPut("localizations")]
    public override async Task HandleAsync([FromBody] ArticleModel request, CancellationToken ct)
    {
        var localization = await _database.Set<Article>().FirstOr404Async(l => l.Id == request.Id, ct);
        var now = DateTimeOffset.UtcNow;

        if (request.Status is ContentStatus.Banned && localization.Status is not ContentStatus.Banned)
            localization.Banned = now;
        else if (request.Status is ContentStatus.Published && localization.Status is not ContentStatus.Published)
            localization.Published = now;

        localization.Status = request.Status;
        localization.BanReason = request.Status is ContentStatus.Banned
            ? request.BanReason
            : null;
        localization.Title = request.Title;
        localization.Description = request.Description;
        localization.Html = request.Html;
        localization.Updated = now;

        await _database.SaveChangesAsync(ct);
    }
}