using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Articles;

namespace NetHub.Admin.Api.Endpoints.Articles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Articles)]
[Authorize(Policy = Policies.HasReadArticlesPermission)]
public class ArticleByIdEndpoint : Endpoint<long, ArticleModel>
{
    private readonly ISqlServerDatabase _database;
    public ArticleByIdEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("articles/{id:long}"), ClientSide(ActionName = "getById")]
    public override async Task<ArticleModel> HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var article = await _database.Set<Article>()
            .Include(a => a.Tags)!.ThenInclude(t => t.Tag)
            .AsNoTracking()
            .Where(u => u.Id == id).FirstOr404Async(ct);

        return article.Adapt<ArticleModel>();
    }
}