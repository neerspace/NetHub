using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.Articles;

[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleVoteGetEndpoint : Endpoint<long, RatingModel>
{
    [HttpGet("me/articles/{articleId:long}/vote"), ClientSide(ActionName = "getVote")]
    public override async Task<RatingModel> HandleAsync([FromRoute(Name = "id")] long articleId, CancellationToken ct)
    {
        var rating = await Database.Set<ArticleVote>()
            .Include(ar => ar.Article)
            .FirstOrDefaultAsync(ar => ar.ArticleId == articleId, ct);

        return rating is null ? new RatingModel(null) : new RatingModel(rating.Vote);
    }
}