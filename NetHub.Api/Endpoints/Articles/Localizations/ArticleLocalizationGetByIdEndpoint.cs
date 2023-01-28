using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Admin.Api.Abstractions;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Api.Endpoints.Articles.Localizations;

internal sealed class ArticleLocalizationGetByIdEndpoint : Endpoint<GetArticleLocalizationRequest, ArticleLocalizationModel>
{
    public override async Task<ArticleLocalizationModel> HandleAsync(GetArticleLocalizationRequest request, CancellationToken ct)
    {
        var userId = UserProvider.TryGetUserId();

        var entity = await Database.Set<ArticleLocalization>()
            .Include(l => l.Contributors).ThenInclude(c => c.User)
            .FirstOrDefaultAsync(l =>
                l.ArticleId == request.ArticleId
                && l.LanguageCode == request.LanguageCode, ct);

        if (entity is null)
            throw new NotFoundException("No such article localization");

        GuardPermissions(entity, userId);

        var localization = entity.Adapt<ArticleLocalizationModel>();

        if (userId is not null)
        {
            var isSaved = await Database.Set<SavedArticle>()
                .SingleOrDefaultAsync(sa => sa.LocalizationId == localization.Id && sa.UserId == userId, ct);
            var articleVote = await Database.Set<ArticleVote>()
                .SingleOrDefaultAsync(sa => sa.ArticleId == localization.ArticleId && sa.UserId == userId, ct);

            localization.IsSaved = isSaved != null;
            localization.SavedDate = isSaved?.SavedDate;
            localization.Vote = articleVote?.Vote;
        }

        localization.Views++;
        await Database.SaveChangesAsync(ct); // TODO: why this task wasn't awaited?

        return localization.Adapt<ArticleLocalizationModel>();
    }

    private static void GuardPermissions(ArticleLocalization localization, long? userId)
    {
        if (localization.Status == ContentStatus.Published)
            return;

        if (userId is null || !localization.Contributors.Select(c => c.UserId).Contains(userId.Value))
            throw new PermissionsException();
    }
}