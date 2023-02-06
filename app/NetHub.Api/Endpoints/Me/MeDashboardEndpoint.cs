using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.Users;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public class MeDashboardEndpoint : ResultEndpoint<DashboardResult>
{
    private readonly ISqlServerDatabase _database;
    public MeDashboardEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("me/dashboard")]
    public override async Task<DashboardResult> HandleAsync(CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var articlesCount = await _database.Set<ArticleContributor>()
            .Where(ac => ac.UserId == userId)
            .CountAsync(ct);
        var articlesViews = await _database.Set<ArticleLocalization>()
            .Include(ar => ar.Contributors)
            .Where(ar => ar.Contributors.FirstOrDefault(c => c.UserId == userId) != null
                && ar.Status == ContentStatus.Published)
            .SumAsync(al => al.Views, ct);

        return new(articlesCount, articlesViews);
    }
}