using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Admin.Api.Endpoints.Localizations;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Localizations)]
[Authorize(Policy = Policies.HasManageArticlesPermission)]
public class LocalizationUpdateEndpoint : ActionEndpoint<ArticleLocalization>
{
    private readonly ISqlServerDatabase _database;

    public LocalizationUpdateEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpPut("localizations")]
    public override async Task HandleAsync([FromBody] ArticleLocalization request, CancellationToken ct)
    {
        var localization = await _database.Set<ArticleLocalization>().FirstOr404Async(l => l.Id == request.Id, ct);
        var now = DateTimeOffset.UtcNow;

        if (request.Status is ContentStatus.Banned && localization.Status is not ContentStatus.Banned)
            localization.Banned = now;
        else if (request.Status is ContentStatus.Published && localization.Status is not ContentStatus.Published)
            localization.Published = now;

        localization.Status = request.Status;
        localization.Title = request.Title;
        localization.Description = request.Description;
        localization.Html = request.Html;
        localization.Updated = now;

        await _database.SaveChangesAsync(ct);
    }
}