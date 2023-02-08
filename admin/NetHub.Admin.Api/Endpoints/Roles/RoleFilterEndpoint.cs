using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Models.Roles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Swagger;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Roles;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Roles)]
[Authorize(Policy = Policies.HasReadRolesPermission)]
public class RoleFilterEndpoint : FilterEndpoint<RoleModel>
{
    private readonly IFilterService _filterService;
    public RoleFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("roles"), ClientSide(ActionName = "filter")]
    public override Task<Filtered<RoleModel>> HandleAsync([FromQuery] FilterRequest request, CancellationToken ct)
    {
        return _filterService.FilterWithCountAsync<AppRole, RoleModel>(request, ct);
    }
}