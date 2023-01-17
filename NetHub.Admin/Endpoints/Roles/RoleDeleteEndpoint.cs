using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Abstractions;
using NetHub.Api.Shared;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasManageRolesPermission)]
public class RoleDeleteEndpoint : ActionEndpoint<long>
{
    private readonly ISqlServerDatabase _database;
    public RoleDeleteEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpDelete("roles/{id:long}")]
    public override async Task HandleAsync([FromRoute] long id, CancellationToken ct = default)
    {
        var role = await _database.Set<AppRole>().FirstOr404Async(r => r.Id == id, ct);
        _database.Set<AppRole>().Remove(role);
        await _database.SaveChangesAsync(ct);
    }
}