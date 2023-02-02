using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities.Views;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.Me.SavedArticles;

[Authorize]
[Tags(TagNames.MySavedArticles)]
[ApiVersion(Versions.V1)]
public sealed class SavedArticleListEndpoint : Endpoint<ArticleLocalizationQuery, ViewLocalizationModel[]>
{
    [HttpGet("me/saved-articles")]
    public override async Task<ViewLocalizationModel[]> HandleAsync(ArticleLocalizationQuery request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var saved = await Database.Set<ViewUserArticle>()
            .Where(ea => ea.UserId == userId
                    && ea.IsSaved == true
                //TODO: Remove comments in release (please...)
                // && ea.Status == ContentStatus.Published
            )
            .ProjectToType<ViewLocalizationModel>()
            .ToArrayAsync(ct);

        return saved.DistinctBy(s => s.LocalizationId).ToArray();
    }
}