using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;

namespace NetHub.Api.Endpoints.ArticleSets.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleStatusUpdateEndpoint : ActionEndpoint<ArticleStatusUpdateRequest>
{
    [HttpPatch("articles/{id:long}/{lang:alpha:length(2)}/status"), ClientSide(ActionName = "updateStatus")]
    public override async Task HandleAsync(ArticleStatusUpdateRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var article = await Database.Set<Article>()
            .Include(al => al.Contributors)
            .FirstOr404Async(al => al.ArticleSetId == request.Id
                && al.LanguageCode == request.LanguageCode, ct);

        if (article.GetAuthorId() != userId)
            throw new PermissionsException();

        CheckStatuses(request, article);

        await Database.SaveChangesAsync(ct);
    }

    private static void CheckStatuses(ArticleStatusUpdateRequest request, Article article)
    {
        if (request.Status == ArticleStatusActions.Publish)
        {
            if (article.Status == ContentStatus.Published)
                return;

            if (article.Status is not (ContentStatus.Draft or ContentStatus.Pending))
                throw new PermissionsException();
            article.Status = ContentStatus.Pending;
        }

        if (request.Status == ArticleStatusActions.UnPublish)
        {
            if (article.Status is not (ContentStatus.Draft or ContentStatus.Published or ContentStatus.Pending))
                throw new PermissionsException();
            article.Status = ContentStatus.Draft;
        }
    }
}