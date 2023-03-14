using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Admin.Api.Endpoints.Articles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Localizations)]
[Authorize(Policy = Policies.HasManageArticlesPermission)]
public class ArticleUpdateEndpoint : ActionEndpoint<Article>
{
    private readonly ISqlServerDatabase _database;
    public ArticleUpdateEndpoint(ISqlServerDatabase database) => _database = database;

    [HttpPut("localizations")]
    public override async Task HandleAsync([FromBody] Article request, CancellationToken ct)
    {
        var article = await _database.Set<Article>().FirstOr404Async(l => l.Id == request.Id, ct);
        request.Adapt(article);
        await _database.SaveChangesAsync(ct);
    }
}