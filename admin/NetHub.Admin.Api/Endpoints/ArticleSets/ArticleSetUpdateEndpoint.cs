using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.ArticleSets;

namespace NetHub.Admin.Api.Endpoints.ArticleSets;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Articles)]
[Authorize(Policy = Policies.HasManageArticlesPermission)]
public class ArticleSetUpdateEndpoint : ActionEndpoint<ArticleSetModel>
{
    private readonly ISqlServerDatabase _database;
    public ArticleSetUpdateEndpoint(ISqlServerDatabase database) => _database = database;

    [HttpPut("articles")]
    public override async Task HandleAsync([FromBody] ArticleSetModel request, CancellationToken ct)
    {
        var articleSet = await _database.Set<ArticleSet>().FirstOr404Async(l => l.Id == request.Id, ct);
        request.Adapt(articleSet);
        await _database.SaveChangesAsync(ct);
    }
}