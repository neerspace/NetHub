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
public class LocalizationDeleteEndpoint : ActionEndpoint<long>
{
    private readonly ISqlServerDatabase _database;
    public LocalizationDeleteEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpDelete("localizations/{id:long}")]
    public override async Task HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var localization = await _database.Set<ArticleLocalization>().FirstOr404Async(l => l.Id == id, ct);
        _database.Set<ArticleLocalization>().Remove(localization);
        await _database.SaveChangesAsync(ct);
    }
}