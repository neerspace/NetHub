using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Admin.Models.Roles;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Api.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasManageRolesPermission)]
public class RoleCreateEndpoint : Endpoint<RoleModel, RoleModel>
{
    private readonly ISqlServerDatabase _database;
    public RoleCreateEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpPost("roles")]
    public override async Task<RoleModel> HandleAsync([FromBody] RoleModel request, CancellationToken ct = default)
    {
        if (await _database.Set<AppRole>().AnyAsync(r => r.NormalizedName == request.Name, ct))
            throw new ValidationFailedException("name", "Role with the same name already exists");

        var role = request.Adapt<AppRole>();
        _database.Set<AppRole>().Add(role);
        await _database.SaveChangesAsync(ct);

        return role.Adapt<RoleModel>();
    }
}