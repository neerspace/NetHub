using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.SavedArticles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class SavedArticleToggleSaveEndpoint : ActionEndpoint<ArticleLocalizationQuery>
{
    [HttpPatch("me/saved-articles/{id:long}/{lang:alpha:length(2)}"), ClientSide(ActionName = "toggleSave")]
    public override async Task HandleAsync(ArticleLocalizationQuery request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var savedArticleEntity = await Database.Set<SavedArticle>()
            .Include(sa => sa.Localization)
            .Where(sa => sa.Localization != null
                && sa.Localization.ArticleId == request.Id
                && sa.Localization.LanguageCode == request.LanguageCode)
            .FirstOrDefaultAsync(ct);

        if (savedArticleEntity is null)
        {
            var localization = await Database.Set<ArticleLocalization>()
                .Where(al => al.ArticleId == request.Id
                    && al.LanguageCode == request.LanguageCode)
                .FirstOr404Async(ct);

            await Database.Set<SavedArticle>().AddAsync(new SavedArticle
            {
                UserId = userId,
                LocalizationId = localization.Id,
            }, ct);

            await Database.SaveChangesAsync(ct);

            return;
        }

        Database.Set<SavedArticle>().Remove(savedArticleEntity);
        await Database.SaveChangesAsync(ct);
    }
}