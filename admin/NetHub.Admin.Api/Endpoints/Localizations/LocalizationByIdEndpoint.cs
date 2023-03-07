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
using NetHub.Shared.Models.Localizations;

namespace NetHub.Admin.Api.Endpoints.Localizations;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Localizations)]
[Authorize(Policy = Policies.HasReadArticlesPermission)]
public class LocalizationByIdEndpoint : Endpoint<long, ArticleLocalizationModel>
{
    private readonly ISqlServerDatabase _database;
    public LocalizationByIdEndpoint(ISqlServerDatabase database) => _database = database;

    [HttpGet("localizations/{id:long}"), ClientSide(ActionName = "getById")]
    public override async Task<ArticleLocalizationModel> HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var localization = await _database.Set<ArticleLocalization>()
            .Include(l => l.Contributors).ThenInclude(c => c.User)
            .AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOr404Async(ct);

        return localization.Adapt<ArticleLocalizationModel>();
    }
}