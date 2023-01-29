using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Models.Articles.Rating;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Api.Endpoints.Articles.Rate;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleRateSetVoteEndpoint : ActionEndpoint<RateArticleRequest>
{
    [HttpPost("articles/{id:long}/rate")]
    public override async Task HandleAsync([FromQuery] RateArticleRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var actualVote = await Database.Set<ArticleVote>()
            .Include(av => av.Article)
            .Where(av => av.ArticleId == request.Id && av.UserId == userId)
            .FirstOrDefaultAsync(ct);

        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id, ct);

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
            return;
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
    }
}