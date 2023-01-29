using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Api.Endpoints.Articles.Rate;

[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleRateGetEndpoint : Endpoint<long, RatingModel>
{
    [HttpGet("articles/{articleId:long}/rate")]
    public override async Task<RatingModel> HandleAsync([FromRoute(Name = "id")] long articleId, CancellationToken ct)
    {
        var rating = await Database.Set<ArticleVote>()
            .Include(ar => ar.Article)
            .FirstOrDefaultAsync(ar => ar.ArticleId == articleId, ct);

        return rating is null ? new RatingModel(null) : new RatingModel(rating.Vote);
    }
}