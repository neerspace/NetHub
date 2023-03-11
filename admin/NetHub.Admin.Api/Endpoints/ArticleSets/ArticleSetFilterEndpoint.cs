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

namespace NetHub.Admin.Api.Endpoints.ArticleSets;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Articles)]
[Authorize(Policy = Policies.HasReadArticlesPermission)]
public sealed class ArticleSetFilterEndpoint : FilterEndpoint<ArticleSetFilterRequest, ArticleSetModel>
{
    private readonly IFilterService _filterService;
    public ArticleSetFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("articles"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<ArticleSetModel>> HandleAsync([FromQuery] ArticleSetFilterRequest request, CancellationToken ct)
    {
        var result = await _filterService.FilterWithCountAsync<ArticleSet, ArticleSetModel>(request, ct,
            x => x.Articles!);
        return result;
    }
}