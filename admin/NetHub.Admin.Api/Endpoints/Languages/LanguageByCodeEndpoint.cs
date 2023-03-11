using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Models.Languages;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Swagger;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Languages;

namespace NetHub.Admin.Api.Endpoints.Languages;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Languages)]
[Authorize(Policy = Policies.HasReadLanguagesPermission)]
public sealed class LanguageByCodeEndpoint : Endpoint<string, LanguageModel>
{
    private readonly ISqlServerDatabase _database;
    public LanguageByCodeEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("languages/{code:alpha:length(2)}"), ClientSide(ActionName = "getByCode")]
    public override async Task<LanguageModel> HandleAsync([FromRoute] string code, CancellationToken ct)
    {
        var language = await _database.Set<Language>().AsNoTracking().FirstOr404Async(l => l.Code == code, ct);
        return language.Adapt<LanguageModel>();
    }
}