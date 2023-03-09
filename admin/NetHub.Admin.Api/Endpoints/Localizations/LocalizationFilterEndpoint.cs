using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Models.Articles;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models;
using NetHub.Shared.Models.Localizations;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Localizations;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Localizations)]
[Authorize(Policy = Policies.HasReadArticlesPermission)]
public class LocalizationFilterEndpoint : FilterEndpoint<ArticleFilterRequest, ArticleLocalizationModel>
{
    private readonly IFilterService _filterService;
    public LocalizationFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("localizations"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<ArticleLocalizationModel>> HandleAsync([FromQuery] ArticleFilterRequest request, CancellationToken ct)
    {
        var result = await _filterService.FilterWithCountAsync<ArticleLocalization, ArticleLocalizationModel>(request, ct);
        return result;
    }
}