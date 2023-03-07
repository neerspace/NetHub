using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Admin.Api.Endpoints.Articles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Articles)]
[Authorize(Policy = Policies.HasReadArticlesPermission)]
public class ArticleLocalizationEndpoint :  Endpoint<long, ArticleLocalizationModel[]>
{
    private readonly ISqlServerDatabase _database;
    public ArticleLocalizationEndpoint(ISqlServerDatabase database) => _database = database;

    [HttpGet("articles/{id:long}/localizations"), ClientSide(ActionName = "getByArticleId")]
    public override async Task<ArticleLocalizationModel[]> HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var localizations = await _database.Set<ArticleLocalization>()
            .Include(l => l.Contributors).ThenInclude(c => c.User)
            .AsNoTracking()
            .Where(u => u.ArticleId == id)
            .ProjectToType<ArticleLocalizationModel>()
            .ToArrayAsync(ct);

        return localizations;
    }

}