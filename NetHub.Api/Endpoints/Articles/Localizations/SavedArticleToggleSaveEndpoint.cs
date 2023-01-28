using MediatR;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Api.Abstractions;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Api.Endpoints.Articles.Localizations;

internal sealed class SavedArticleToggleSaveEndpoint : ActionEndpoint<ToggleArticleSaveRequest>
{
    public override async Task<Unit> HandleAsync(ToggleArticleSaveRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var savedArticleEntity = await Database.Set<SavedArticle>()
            .Include(sa => sa.Localization)
            .Where(sa => sa.Localization != null
                && sa.Localization.ArticleId == request.ArticleId
                && sa.Localization.LanguageCode == request.LanguageCode)
            .FirstOrDefaultAsync(ct);

        if (savedArticleEntity is null)
        {
            var localization = await Database.Set<ArticleLocalization>()
                .Where(al => al.ArticleId == request.ArticleId
                    && al.LanguageCode == request.LanguageCode)
                .FirstOr404Async(ct);

            await Database.Set<SavedArticle>().AddAsync(new SavedArticle
            {
                UserId = userId,
                LocalizationId = localization.Id,
            }, ct);

            await Database.SaveChangesAsync(ct);

            return Unit.Value;
        }

        Database.Set<SavedArticle>().Remove(savedArticleEntity);
        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }
}