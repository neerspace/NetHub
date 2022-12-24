using MediatR;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Ratings.SetRate;

internal sealed class RateArticleHandler : AuthorizedHandler<RateArticleRequest>
{
    public RateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<Unit> Handle(RateArticleRequest request, CancellationToken ct)
    {
        var userId = UserProvider.GetUserId();

        var actualVote = await Database.Set<ArticleVote>()
            .Include(av => av.Article)
            .Where(av => av.ArticleId == request.ArticleId && av.UserId == userId)
            .FirstOrDefaultAsync(ct);

        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.ArticleId, ct);

        if (actualVote is null)
        {
            var voteEntity = new ArticleVote
            {
                ArticleId = article.Id,
                UserId = userId,
                Vote = request.Vote
            };

            article.Rate += request.Vote == Vote.Up ? 1 : -1;

            Database.Set<ArticleVote>().Add(voteEntity);
            await Database.SaveChangesAsync(ct);

            return Unit.Value;
        }

        switch (request.Vote)
        {
            case Vote.Up:
                //was up
                if (actualVote.Vote == Vote.Up)
                {
                    article.Rate -= 1;
                    Database.Set<ArticleVote>().Remove(actualVote);
                    break;
                }

                //was down
                article.Rate += 2;
                actualVote.Vote = Vote.Up;

                break;
            case Vote.Down:
                //was down
                if (actualVote.Vote == Vote.Down)
                {
                    article.Rate += 1;
                    Database.Set<ArticleVote>().Remove(actualVote);
                    break;
                }

                //was up
                article.Rate -= 2;
                actualVote.Vote = Vote.Down;

                break;
        }

        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }
}