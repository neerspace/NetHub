using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Extensions;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Articles;

namespace NetHub.Api.Endpoints.Me.Articles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public class MeArticleGetEndpoint: ResultEndpoint<ArticleModel[]>
{
    [HttpGet("me/articles")]
    public override async Task<ArticleModel[]> HandleAsync(CancellationToken ct)
    {
        long userId = UserProvider.UserId;

        var articles = await Database
            .GetExtendedArticles(userId, whereExpression: a => a.ArticleSet!.AuthorId == userId)
            .ProjectToType<ArticleModel>()
            .ToArrayAsync(ct);

        return articles;
    }
}