using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Roles;
using NetHub.Admin.Swagger;
using NetHub.Api.Shared;
using NetHub.Application.Interfaces;
using NetHub.Application.Models;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasReadRolesPermission)]
public class RoleFilterEndpoint : FilterEndpoint<RoleModel>
{
    private readonly IFilterService _filterService;
    public RoleFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("roles"), ClientSide(ActionName = "filter")]
    public override Task<Filtered<RoleModel>> HandleAsync([FromQuery] FilterRequest request, CancellationToken ct = default)
    {
        return _filterService.FilterWithCountAsync<AppRole, RoleModel>(request, ct);
    }
}