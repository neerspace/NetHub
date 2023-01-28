using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Api.Abstractions;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Api.Endpoints.Articles.Localizations;

internal sealed class ArticleLocalizationGetSavingEndpoint : Endpoint<GetLocalizationSavingRequest, GetLocalizationSavingResult>
{
    public override async Task<GetLocalizationSavingResult> HandleAsync(GetLocalizationSavingRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var savedLocalization = await Database.Set<SavedArticle>()
            .Include(sa => sa.Localization)
            .SingleOrDefaultAsync(sa =>
                sa.UserId == userId
                && sa.Localization!.ArticleId == request.ArticleId
                && sa.Localization.LanguageCode == request.LanguageCode, ct);

        return new(savedLocalization is not null);
    }
}