using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Admin.Api.Abstractions;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Api.Endpoints.Articles.Localizations;

internal sealed class ArticleLocalizationDeleteEndpoint : ActionEndpoint<DeleteArticleLocalizationRequest>
{
    public override async Task HandleAsync(DeleteArticleLocalizationRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var localization = await Database.Set<ArticleLocalization>()
            .Include(al => al.Contributors)
            .SingleOrDefaultAsync(al => al.ArticleId == request.ArticleId && al.LanguageCode == request.LanguageCode, ct);

        if (localization is null)
            throw new NotFoundException("No such article localization");

        if (localization.Contributors.First(a => a.Role == ArticleContributorRole.Author).UserId != userId)
            throw new PermissionsException();

        Database.Set<ArticleLocalization>().Remove(localization);

        await Database.SaveChangesAsync(ct);
    }
}