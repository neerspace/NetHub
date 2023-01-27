using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Models.Users;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public class UserDashboardEndpoint : Endpoint<string, DashboardDto>
{
    private readonly ISqlServerDatabase _database;
    public UserDashboardEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("users/{username}/dashboard")]
    public override async Task<DashboardDto> HandleAsync([FromRoute] string username, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(username))
            throw new ValidationFailedException("Username must not be null or empty.");

        int articlesCount = await _database.Set<ArticleContributor>()
            .Include(ac => ac.User)
            .Include(ac => ac.Localization)
            .Where(ac => ac.User!.NormalizedUserName == username.ToUpper()
                && ac.Localization!.Status == ContentStatus.Published)
            .CountAsync(ct);

        int articlesViews = await _database.Set<ArticleLocalization>()
            .Include(ar => ar.Contributors).ThenInclude(ac => ac.User)
            .Where(ar => ar.Contributors.FirstOrDefault(c => c.User!.NormalizedUserName == username.ToUpper()) != null
                && ar.Status == ContentStatus.Published)
            .SumAsync(al => al.Views, ct);

        return new(articlesCount, articlesViews);
    }
}