using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Models.Articles.Localizations;
using NetHub.Models.Me.SavedArticles;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.SavedArticles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class IsSavedArticleEndpoint : Endpoint<ArticleLocalizationQuery, IsSavedLocalizationResult>
{
    [HttpGet("me/saved-articles/{id:long}/{lang:alpha}"), ClientSide(ActionName = "isSaved")]
    public override async Task<IsSavedLocalizationResult> HandleAsync(ArticleLocalizationQuery request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var savedLocalization = await Database.Set<SavedArticle>()
            .Include(sa => sa.Localization)
            .SingleOrDefaultAsync(sa =>
                sa.UserId == userId
                && sa.Localization!.ArticleId == request.Id
                && sa.Localization.LanguageCode == request.LanguageCode, ct);

        return new(savedLocalization is not null);
    }
}