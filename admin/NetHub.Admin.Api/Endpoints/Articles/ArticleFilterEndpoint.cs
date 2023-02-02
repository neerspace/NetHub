using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Models.Articles;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models;
using NetHub.Shared.Models.Articles;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Articles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Articles)]
[Authorize(Policy = Policies.HasReadArticlesPermission)]
public sealed class ArticleFilterEndpoint : FilterEndpoint<ArticleFilterRequest, ArticleModel>
{
    private readonly IFilterService _filterService;
    public ArticleFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("articles"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<ArticleModel>> HandleAsync([FromQuery] ArticleFilterRequest request, CancellationToken ct)
    {
        var result = await _filterService.FilterWithCountAsync<Article, ArticleModel>(request, ct);
        return result;
    }
}