using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me.Articles;

[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleVoteGetEndpoint : Endpoint<long, VoteModel>
{
    [HttpGet("me/articles/{articleId:long}/vote"), ClientSide(ActionName = "getVote")]
    public override async Task<VoteModel> HandleAsync([FromRoute(Name = "id")] long articleId, CancellationToken ct)
    {
        var voteEntity = await Database.Set<ArticleSetVote>()
            .Include(ar => ar.ArticleSet)
            .FirstOrDefaultAsync(ar => ar.ArticleSetId == articleId, ct);

        return voteEntity is null ? new VoteModel(null) : new VoteModel(voteEntity.Vote);
    }
}