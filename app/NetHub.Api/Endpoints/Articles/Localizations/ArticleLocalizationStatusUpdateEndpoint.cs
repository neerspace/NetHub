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
public sealed class ArticleLocalizationStatusUpdateEndpoint : ActionEndpoint<SetArticleStatusRequest>
{
    [HttpPatch("articles/{id:long}/{lang:alpha:length(2)}/status"), ClientSide(ActionName = "updateStatus")]
    public override async Task HandleAsync(SetArticleStatusRequest statusRequest, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var localization = await Database.Set<ArticleLocalization>()
            .Include(al => al.Contributors)
            .FirstOr404Async(al => al.ArticleId == statusRequest.Id
                && al.LanguageCode == statusRequest.LanguageCode, ct);

        CheckStatuses(statusRequest, localization, userId);

        await Database.SaveChangesAsync(ct);
    }

    private static void CheckStatuses(SetArticleStatusRequest request, ArticleLocalization localization, long userId)
    {
        if (request.Status == ArticleStatusRequest.Publish)
        {
            if (localization.Status == ContentStatus.Published)
                return;

            if (localization.GetAuthorId() != userId
                || localization.Status is not (ContentStatus.Draft or ContentStatus.Pending))
                throw new PermissionsException();
            localization.Status = ContentStatus.Pending;
        }

        if (request.Status == ArticleStatusRequest.UnPublish)
        {
            if (localization.GetAuthorId() != userId
                || localization.Status is not (ContentStatus.Draft or ContentStatus.Published or ContentStatus.Pending))
                throw new PermissionsException();
            localization.Status = ContentStatus.Draft;
        }
    }
}