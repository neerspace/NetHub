using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Models.Languages;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Swagger;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Languages;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Languages)]
[Authorize(Policy = Policies.HasReadLanguagesPermission)]
public sealed class LanguageFilterEndpoint : FilterEndpoint<LanguageFilterRequest, LanguageModel>
{
    private readonly IFilterService _filterService;
    public LanguageFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("languages"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<LanguageModel>> HandleAsync([FromQuery] LanguageFilterRequest request, CancellationToken ct)
    {
        return await _filterService.FilterWithCountAsync<Language, LanguageModel>(request, ct);
    }
}