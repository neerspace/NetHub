using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Api.Abstractions;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Data.SqlServer.Entities.Views;

namespace NetHub.Api.Endpoints.Articles.Localizations;

internal sealed class ArticleGetSavedEndpoint : Endpoint<GetSavedArticlesRequest, ExtendedArticleModel[]>
{
    public override async Task<ExtendedArticleModel[]> HandleAsync(GetSavedArticlesRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var saved = await Database.Set<ExtendedUserArticle>()
            .Where(ea => ea.UserId == userId
                    && ea.IsSaved == true
                //TODO: Remove comments in release (please...)
                // && ea.Status == ContentStatus.Published
            )
            .ProjectToType<ExtendedArticleModel>()
            .ToArrayAsync(ct);

        return saved.DistinctBy(s => s.LocalizationId).ToArray();
    }
}