using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Articles;

namespace NetHub.Admin.Api.Endpoints.ArticleSets;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Articles)]
[Authorize(Policy = Policies.HasReadArticlesPermission)]
public class ArticleEndpoint :  Endpoint<long, ArticleModel[]>
{
    private readonly ISqlServerDatabase _database;
    public ArticleEndpoint(ISqlServerDatabase database) => _database = database;

    [HttpGet("articles/{id:long}/localizations"), ClientSide(ActionName = "getBySetId")]
    public override async Task<ArticleModel[]> HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var articles = await _database.Set<Article>()
            .Include(l => l.Contributors).ThenInclude(c => c.User)
            .AsNoTracking()
            .Where(u => u.ArticleSetId == id)
            .ProjectToType<ArticleModel>()
            .ToArrayAsync(ct);

        return articles;
    }

}