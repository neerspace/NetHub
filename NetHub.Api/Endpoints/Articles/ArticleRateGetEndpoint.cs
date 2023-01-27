using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Endpoints.Articles.Ratings.Get;
using NetHub.Api.Shared;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Api.Endpoints.Articles;

[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleRateGetEndpoint : Endpoint<GetArticleRateRequest, RatingModel>
{
    [HttpGet("articles/{id:long}/rate")]
    public override async Task<RatingModel> HandleAsync(GetArticleRateRequest request, CancellationToken ct)
    {
        var rating = await Database.Set<ArticleVote>()
            .Include(ar => ar.Article)
            .FirstOrDefaultAsync(ar => ar.ArticleId == request.ArticleId, ct);

        return rating is null ? new RatingModel(null) : new RatingModel(rating.Vote);
    }
}