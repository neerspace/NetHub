using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Models.Me.SavedArticles;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.SavedArticles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class IsSavedArticleEndpoint : Endpoint<ArticleQuery, IsSavedArticleResult>
{
    [HttpGet("me/saved-articles/{id:long}/{lang:alpha}"), ClientSide(ActionName = "isSaved")]
    public override async Task<IsSavedArticleResult> HandleAsync(ArticleQuery request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var savedArticle = await Database.Set<SavedArticle>()
            .Include(sa => sa.Article)
            .SingleOrDefaultAsync(sa =>
                sa.UserId == userId
                && sa.Article!.ArticleSetId == request.Id
                && sa.Article.LanguageCode == request.LanguageCode, ct);

        return new(savedArticle is not null);
    }
}