using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
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
        request.Adapt(localization);
        await _database.SaveChangesAsync(ct);
    }
}