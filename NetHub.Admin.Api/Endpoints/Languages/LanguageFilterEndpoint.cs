using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Models.Languages;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Swagger;
using NetHub.Application.Interfaces;
using NetHub.Application.Models;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Admin.Api.Endpoints.Languages;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Languages)]
[Authorize(Policy = Policies.HasReadLanguagesPermission)]
public sealed class LanguageFilterEndpoint : FilterEndpoint<LanguageFilterRequest, LanguageModel>
{
    private readonly IFilterService _filterService;
    public LanguageFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("languages"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<LanguageModel>> HandleAsync([FromQuery] LanguageFilterRequest request, CancellationToken ct = default)
    {
        return await _filterService.FilterWithCountAsync<Language, LanguageModel>(request, ct);
    }
}