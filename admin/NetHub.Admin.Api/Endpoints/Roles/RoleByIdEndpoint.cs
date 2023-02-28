using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Models.Roles;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Admin.Api.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasReadRolesPermission)]
public class RoleByIdEndpoint : Endpoint<long, RoleModel>
{
    private readonly ISqlServerDatabase _database;
    public RoleByIdEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("roles/{id:long}"), ClientSide(ActionName = "getById")]
    public override async Task<RoleModel> HandleAsync([FromRoute] long id, CancellationToken ct)
    {
        var role = await _database.Set<AppRole>()
            .Include(r => r.RoleClaims!.Where(rc => rc.ClaimType == Claims.Permission))
            .FirstOr404Async(r => r.Id == id, ct);

        return role.Adapt<RoleModel>();
    }
}