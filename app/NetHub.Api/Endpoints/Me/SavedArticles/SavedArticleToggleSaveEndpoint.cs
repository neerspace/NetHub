using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.SavedArticles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class SavedArticleToggleSaveEndpoint : ActionEndpoint<ArticleQuery>
{
    [HttpPatch("me/saved-articles/{id:long}/{lang:alpha:length(2)}"), ClientSide(ActionName = "toggleSave")]
    public override async Task HandleAsync(ArticleQuery request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var savedArticleEntity = await Database.Set<SavedArticle>()
            .Include(sa => sa.Article)
            .Where(sa => sa.Article != null
                && sa.Article.ArticleSetId == request.Id
                && sa.Article.LanguageCode == request.LanguageCode)
            .FirstOrDefaultAsync(ct);

        if (savedArticleEntity is null)
        {
            var localization = await Database.Set<Article>()
                .Where(al => al.ArticleSetId == request.Id
                    && al.LanguageCode == request.LanguageCode)
                .FirstOr404Async(ct);

            await Database.Set<SavedArticle>().AddAsync(new SavedArticle
            {
                UserId = userId,
                ArticleId = localization.Id,
            }, ct);

            await Database.SaveChangesAsync(ct);

            return;
        }

        Database.Set<SavedArticle>().Remove(savedArticleEntity);
        await Database.SaveChangesAsync(ct);
    }
}