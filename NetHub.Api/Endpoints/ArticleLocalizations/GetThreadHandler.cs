using NetHub.Application.Services;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Views;

namespace NetHub.Application.Models.Articles.Localizations.Many;

internal sealed class GetThreadHandler : DbHandler<GetThreadRequest, ExtendedArticleModel[]>
{
    private readonly IUserProvider _userProvider;
    private readonly IFilterService _filterService;

    public GetThreadHandler(IServiceProvider serviceProvider, IFilterService filterService) : base(serviceProvider)
    {
        _filterService = filterService;
        _userProvider = serviceProvider.GetRequiredService<IUserProvider>();
    }


    public override async Task<ExtendedArticleModel[]> Handle(GetThreadRequest request, CancellationToken ct)
    {
        var userId = _userProvider.TryGetUserId();

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