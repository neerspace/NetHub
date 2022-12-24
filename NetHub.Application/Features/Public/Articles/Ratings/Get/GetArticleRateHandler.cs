using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Public.Articles.Localizations;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Application.Features.Public.Articles.Ratings.Get;

internal sealed class GetArticleRateHandler : AuthorizedHandler<GetArticleRateRequest, RatingModel>
{
    public GetArticleRateHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<RatingModel> Handle(GetArticleRateRequest request, CancellationToken ct)
    {
        var rating = await Database.Set<ArticleVote>()
            .Include(ar => ar.Article)
            .FirstOrDefaultAsync(ar => ar.ArticleId == request.ArticleId, ct);

        return rating is null ? new RatingModel(null) : new RatingModel(rating.Vote);
    }
}