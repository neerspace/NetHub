using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Api.Endpoints.Users;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Users)]
[Authorize(Policy = Policies.HasManageUsersPermission)]
public sealed class UserDeleteEndpoint : ActionEndpoint<long>
{
    private readonly ISqlServerDatabase _database;
    public UserDeleteEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpDelete("users/{id:long}")]
    public override async Task HandleAsync([FromRoute] long id, CancellationToken ct = default)
    {
        var user = await _database.Set<AppUser>().FirstOr404Async(u => u.Id == id, ct);
        _database.Set<AppUser>().Remove(user);
        await _database.SaveChangesAsync(ct);
    }
}