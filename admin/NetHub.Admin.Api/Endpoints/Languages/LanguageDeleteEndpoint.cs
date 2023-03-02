using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Api.Constants;

namespace NetHub.Admin.Api.Endpoints.Languages;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Languages)]
[Authorize(Policy = Policies.HasManageLanguagesPermission)]
public sealed class LanguageDeleteEndpoint : ActionEndpoint<string>
{
    private readonly ISqlServerDatabase _database;
    public LanguageDeleteEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpDelete("languages/{code:alpha:length(2)}")]
    public override async Task HandleAsync([FromRoute] string code, CancellationToken ct)
    {
        var language = await _database.Set<Language>().FirstOr404Async(l => l.Code == code, ct);
        _database.Set<Language>().Remove(language);
        await _database.SaveChangesAsync(ct);
    }
}