using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api.Constants;

namespace NetHub.Admin.Api.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasManageRolesPermission)]
public class RoleDeleteEndpoint : ActionEndpoint<long>
{
    private readonly ISqlServerDatabase _database;
    public RoleDeleteEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpDelete("roles/{id:long}")]
    public override async Task HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var role = await _database.Set<AppRole>().FirstOr404Async(r => r.Id == id, ct);
        _database.Set<AppRole>().Remove(role);
        await _database.SaveChangesAsync(ct);
    }
}