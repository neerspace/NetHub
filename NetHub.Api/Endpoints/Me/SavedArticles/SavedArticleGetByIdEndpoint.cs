using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Api.Endpoints.Me.SavedArticles;

[Authorize]
[Tags(TagNames.MySavedArticles)]
[ApiVersion(Versions.V1)]
public sealed class SavedArticleGetByIdEndpoint : Endpoint<GetLocalizationSavingRequest, GetLocalizationSavingResult>
{
    [HttpGet("me/saved-articles/{id:long}/{lang:alpha}")]
    public override async Task<GetLocalizationSavingResult> HandleAsync(GetLocalizationSavingRequest request, CancellationToken ct)
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