using NetHub.Admin.Api.Abstractions;
using NetHub.Application.Models;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Application.Services;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Views;

namespace NetHub.Api.Endpoints.Articles.Localizations;

internal sealed class ArticleLocalizationGetThreadEndpoint : Endpoint<GetThreadRequest, ExtendedArticleModel[]>
{
    private readonly IFilterService _filterService;
    public ArticleLocalizationGetThreadEndpoint(IFilterService filterService) => _filterService = filterService;


    public override async Task<ExtendedArticleModel[]> HandleAsync(GetThreadRequest request, CancellationToken ct)
    {
        var userId = UserProvider.TryGetUserId();

        var result = userId != null
            ? GetExtendedArticles(request, ct, userId.Value)
            : GetSimpleArticles(request, ct);

        return await result;
    }

    private async Task<ExtendedArticleModel[]> GetSimpleArticles(FilterRequest request, CancellationToken cancel)
    {
        if (request.Filters != null && request.Filters.Contains("contributorRole"))
            request.Filters = request.Filters.Replace(",contributorRole==Author", "");

        if (request.Filters != null && request.Filters.Contains("contributorId"))
            request.Filters = request.Filters.Replace("contributorId", "InContributors");

        request.Filters += ",status==published";
        request.Sorts = "published";

        var result = await _filterService
            .FilterAsync<ArticleLocalization, ExtendedArticleModel>(request, cancel, al => al.Contributors);

        return result;
    }

    private async Task<ExtendedArticleModel[]> GetExtendedArticles(FilterRequest request, CancellationToken cancel,
        long userId)
    {
        request.Filters += $",userId=={userId}";
        request.Filters += ",status==published";
        request.Sorts = "published";

        var result =
            await _filterService.FilterAsync<ExtendedUserArticle, ExtendedArticleModel>(request, ct: cancel);
        return result;
    }
}