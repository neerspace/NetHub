using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Users;
using NetHub.Admin.Swagger;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Endpoints.Users;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Users)]
// [Authorize(Policy = Policies.HasManageUsersPermission)]
[AllowAnonymous]
public sealed class UserByIdEndpoint : Endpoint<long, User>
{
    private readonly IDatabase _database;
    public UserByIdEndpoint(IDatabase database) => _database = database;


    [HttpGet("users/{id:long}"), ClientSide(ActionName = "getById")]
    public override async Task<User> HandleAsync([FromRoute] long id, CancellationToken ct = default)
    {
        return await _database.Set<AppUser>()
            .AsNoTracking()
            .Where(e => e.Id == id)
            .ProjectToType<User>()
            .FirstAsync(ct); // TODO: Update NeerCore and use FirstOr404Async here
    }
}