using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.ArticleSets.Rating;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.Articles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleVoteUpdateEndpoint : ActionEndpoint<ArticleVoteUpdateRequest>
{
    [HttpPost("me/articles/{Id:long}/vote"), ClientSide(ActionName = "updateVote")]
    public override async Task HandleAsync(ArticleVoteUpdateRequest voteUpdateRequest, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var actualVote = await Database.Set<ArticleSetVote>()
            .Include(av => av.ArticleSet)
            .Where(av => av.ArticleSetId == voteUpdateRequest.Id && av.UserId == userId)
            .FirstOrDefaultAsync(ct);

        var article = await Database.Set<ArticleSet>().FirstOr404Async(a => a.Id == voteUpdateRequest.Id, ct);

        if (actualVote is null)
        {
            var voteEntity = new ArticleSetVote
            {
                ArticleSetId = article.Id,
                UserId = userId,
                Vote = voteUpdateRequest.Vote
            };

            article.Rate += voteUpdateRequest.Vote == Vote.Up ? 1 : -1;

            Database.Set<ArticleSetVote>().Add(voteEntity);
            await Database.SaveChangesAsync(ct);
            return;
        }

        switch (voteUpdateRequest.Vote)
        {
            case Vote.Up:
                //was up
                if (actualVote.Vote == Vote.Up)
                {
                    article.Rate -= 1;
                    Database.Set<ArticleSetVote>().Remove(actualVote);
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
                    Database.Set<ArticleSetVote>().Remove(actualVote);
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