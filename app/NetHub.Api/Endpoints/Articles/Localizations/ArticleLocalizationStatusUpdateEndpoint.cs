using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;

namespace NetHub.Api.Endpoints.Articles.Localizations;

[Authorize]
[Tags(TagNames.ArticleLocalizations)]
[ApiVersion(Versions.V1)]
public sealed class ArticleLocalizationStatusUpdateEndpoint : ActionEndpoint<ArticleLocalizationStatusUpdateRequest>
{
    [HttpPatch("articles/{Id:long}/{Language:alpha:length(2)}/status"), ClientSide(ActionName = "updateStatus")]
    public override async Task HandleAsync(ArticleLocalizationStatusUpdateRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var localization = await Database.Set<ArticleLocalization>()
            .Include(al => al.Contributors)
            .FirstOr404Async(al => al.ArticleId == request.Id
                && al.LanguageCode == request.Language, ct);

        if (localization.GetAuthorId() != userId)
            throw new PermissionsException();

        CheckStatuses(request, localization);

        await Database.SaveChangesAsync(ct);
    }

    private static void CheckStatuses(ArticleLocalizationStatusUpdateRequest request, ArticleLocalization localization)
    {
        if (request.Status == ArticleStatusActions.Publish)
        {
            if (localization.Status == ContentStatus.Published)
                return;

            if (localization.Status is not (ContentStatus.Draft or ContentStatus.Pending))
                throw new PermissionsException();
            localization.Status = ContentStatus.Pending;
        }

        if (request.Status == ArticleStatusActions.UnPublish)
        {
            if (localization.Status is not (ContentStatus.Draft or ContentStatus.Published or ContentStatus.Pending))
                throw new PermissionsException();
            localization.Status = ContentStatus.Draft;
        }
    }
}