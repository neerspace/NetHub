using Microsoft.AspNetCore.Mvc;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Views;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models;
using NetHub.Shared.Services;

namespace NetHub.Api.Endpoints.Articles.Localizations;

[Tags(TagNames.ArticleLocalizations)]
[ApiVersion(Versions.V1)]
public sealed class ArticleLocalizationSearchEndpoint : Endpoint<ArticleLocalizationFilter, ViewLocalizationModel[]>
{
    private readonly IFilterService _filterService;
    public ArticleLocalizationSearchEndpoint(IFilterService filterService) => _filterService = filterService;

    [HttpGet("articles/{languageCode:alpha:length(2)}/search")]
    public override async Task<ViewLocalizationModel[]> HandleAsync(ArticleLocalizationFilter request, CancellationToken ct)
    {
        var userId = UserProvider.TryGetUserId();

        var result = userId != null
            ? GetExtendedArticles(request, ct, userId.Value)
            : GetSimpleArticles(request, ct);

        return await result;
    }

    private async Task<ViewLocalizationModel[]> GetSimpleArticles(FilterRequest request, CancellationToken cancel)
    {
        if (request.Filters != null && request.Filters.Contains("contributorRole"))
            request.Filters = request.Filters.Replace(",contributorRole==Author", "");

        if (request.Filters != null && request.Filters.Contains("contributorId"))
            request.Filters = request.Filters.Replace("contributorId", "InContributors");

        request.Filters += ",status==published";
        request.Sorts = "published";

        var result = await _filterService
            .FilterAsync<ArticleLocalization, ViewLocalizationModel>(request, cancel, al => al.Contributors);

        return result;
    }

    private async Task<ViewLocalizationModel[]> GetExtendedArticles(FilterRequest request, CancellationToken cancel,
        long userId)
    {
        request.Filters += $",userId=={userId}";
        request.Filters += ",status==published";
        request.Sorts = "published";

        var result =
            await _filterService.FilterAsync<ViewUserArticle, ViewLocalizationModel>(request, ct: cancel);
        return result;
    }
}