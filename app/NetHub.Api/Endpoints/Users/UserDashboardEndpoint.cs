using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.Users;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public class UserDashboardEndpoint : Endpoint<string, DashboardResult>
{
    private readonly ISqlServerDatabase _database;
    public UserDashboardEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("users/{username}/dashboard")]
    public override async Task<DashboardResult> HandleAsync([FromRoute] string username, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(username))
            throw new ValidationFailedException("Username must not be null or empty.");

        int articlesCount = await _database.Set<ArticleContributor>()
            .Include(ac => ac.User)
            .Include(ac => ac.Article)
            .Where(ac => ac.User!.NormalizedUserName == username.ToUpper())
            .Where(ac => ac.Article!.Status == ContentStatus.Published)
            .CountAsync(ct);

        int articlesViews = await _database.Set<Article>()
            .Include(ar => ar.Contributors).ThenInclude(ac => ac.User)
            .Where(ar => ar.Contributors.Any(c => c.User!.NormalizedUserName == username.ToUpper()))
            .Where(ar => ar.Status == ContentStatus.Published)
            .SumAsync(al => al.Views, ct);

        return new(articlesCount, articlesViews);
    }
}