using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Languages;
using NetHub.Admin.Swagger;
using NetHub.Application.Interfaces;
using NetHub.Application.Models;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Admin.Endpoints.Languages;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Languages)]
// [Authorize(Policy = Policies.HasManageUsersPermission)]
[AllowAnonymous]
public sealed class LanguageFilterEndpoint : FilterEndpoint<LanguageModel>
{
    private readonly IFilterService _filterService;
    public LanguageFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("languages"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<LanguageModel>> HandleAsync([FromQuery] FilterRequest request, CancellationToken ct = default)
    {
        return await _filterService.FilterWithCountAsync<Language, LanguageModel>(request, ct);
    }
}