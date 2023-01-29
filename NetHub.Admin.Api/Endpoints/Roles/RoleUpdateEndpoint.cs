using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Admin.Models.Roles;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Api.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasManageRolesPermission)]
public class RoleUpdateEndpoint : ActionEndpoint<RoleModel>
{
    private readonly ISqlServerDatabase _database;
    private readonly DbSet<AppRole> _roles;
    private readonly DbSet<AppRoleClaim> _roleClaims;

    public RoleUpdateEndpoint(ISqlServerDatabase database)
    {
        _database = database;
        _roles = _database.Set<AppRole>();
        _roleClaims = _database.Set<AppRoleClaim>();
    }


    [HttpPut("roles")]
    public override async Task<RoleModel> HandleAsync([FromBody] RoleModel request, CancellationToken ct = default)
    {
        if (await _roles.CountAsync(r => r.NormalizedName == request.Name, ct) > 1)
            throw new ValidationFailedException("name", "Role with the same name already exists");

        var role = await _roles.Include(r => r.RoleClaims!.Where(rc => rc.ClaimType == Claims.Permission))
            .FirstOr404Async(r => r.Id == request.Id, ct);

        var prevPermissions = role.RoleClaims!;
        request.Adapt(role);
        var newPermissions = role.RoleClaims!;

        _roleClaims.RemoveRange(prevPermissions.Where(p => !newPermissions.Contains(p)));
        _roleClaims.AddRange(newPermissions.Where(p => !prevPermissions.Contains(p)));

        await _database.SaveChangesAsync(ct);

        return role.Adapt<RoleModel>();
    }
}