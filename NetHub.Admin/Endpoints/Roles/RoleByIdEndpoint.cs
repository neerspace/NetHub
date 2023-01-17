using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Roles;
using NetHub.Admin.Swagger;
using NetHub.Api.Shared;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasReadRolesPermission)]
public class RoleByIdEndpoint : Endpoint<long, RoleModel>
{
    private readonly ISqlServerDatabase _database;
    public RoleByIdEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("roles/{id:long}"), ClientSide(ActionName = "getById")]
    public override async Task<RoleModel> HandleAsync([FromRoute] long id, CancellationToken ct = default)
    {
        var role = await _database.Set<AppRole>()
            .Include(r => r.RoleClaims!.Where(rc => rc.ClaimType == Claims.Permission))
            .FirstOr404Async(r => r.Id == id, ct);

        return role.Adapt<RoleModel>();
    }
}