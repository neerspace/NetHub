using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.Articles.Rating;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.Articles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleVoteUpdateEndpoint : ActionEndpoint<RateArticleRequest>
{
    [HttpPost("me/articles/{id:long}/vote"), ClientSide(ActionName = "updateVote")]
    public override async Task HandleAsync(RateArticleRequest request, CancellationToken ct)
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