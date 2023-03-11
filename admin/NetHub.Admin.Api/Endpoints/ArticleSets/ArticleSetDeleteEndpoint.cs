using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Admin.Api.Endpoints.ArticleSets;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Articles)]
[Authorize(Policy = Policies.HasManageArticlesPermission)]
public class ArticleSetDeleteEndpoint : ActionEndpoint<long>
{
    private readonly ISqlServerDatabase _database;
    public ArticleSetDeleteEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpDelete("articles/{id:long}")]
    public override async Task HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var articleSet = await _database.Set<ArticleSet>().FirstOr404Async(l => l.Id == id, ct);
        _database.Set<ArticleSet>().Remove(articleSet);
        await _database.SaveChangesAsync(ct);
    }
}