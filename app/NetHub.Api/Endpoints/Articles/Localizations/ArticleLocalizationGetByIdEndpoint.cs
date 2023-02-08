using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Api.Endpoints.Articles.Localizations;

[Tags(TagNames.ArticleLocalizations)]
[ApiVersion(Versions.V1)]
public sealed class ArticleLocalizationGetByIdEndpoint : Endpoint<ArticleLocalizationQuery, ViewLocalizationModel>
{
    [HttpGet("articles/{id:long}/{lang:alpha:length(2)}"), ClientSide(ActionName = "getByIdAndCode")]
    public override async Task<ViewLocalizationModel> HandleAsync(ArticleLocalizationQuery request, CancellationToken ct)
    {
        var userId = UserProvider.TryGetUserId();

        var entity = await Database.Set<ArticleLocalization>()
            .Include(l => l.Contributors).ThenInclude(c => c.User)
            .FirstOrDefaultAsync(l =>
                l.ArticleId == request.Id
                && l.LanguageCode == request.LanguageCode, ct);

        if (entity is null)
            throw new NotFoundException("No such article localization");

        GuardPermissions(entity, userId);

        var localization = entity.Adapt<ViewLocalizationModel>();

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

        entity.Views++;
        await Database.SaveChangesAsync(ct);

        return localization;
    }

    private static void GuardPermissions(ArticleLocalization localization, long? userId)
    {
        if (localization.Status == ContentStatus.Published)
            return;

        if (userId is null || !localization.Contributors.Select(c => c.UserId).Contains(userId.Value))
            throw new PermissionsException();
    }
}