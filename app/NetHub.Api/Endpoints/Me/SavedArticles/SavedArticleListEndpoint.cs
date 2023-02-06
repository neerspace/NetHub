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
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class SavedArticleListEndpoint : ResultEndpoint<ViewLocalizationModel[]>
{
    [HttpGet("me/saved-articles")]
    public override async Task<ViewLocalizationModel[]> HandleAsync(CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var saved = await Database.Set<ViewUserArticle>()
            .Where(ea => ea.UserId == userId
                    && ea.IsSaved == true
            )
            .ProjectToType<ViewLocalizationModel>()
            .ToArrayAsync(ct);

        return saved.DistinctBy(s => s.LocalizationId).ToArray();
    }
}