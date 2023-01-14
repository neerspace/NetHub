using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Users;
using NetHub.Admin.Swagger;
using NetHub.Application.Interfaces;
using NetHub.Application.Models;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Endpoints.Users;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Users)]
// [Authorize(Policy = Policies.HasManageUsersPermission)]
[AllowAnonymous]
public sealed class UserFilterEndpoint : FilterEndpoint<UserModel>
{
    private readonly IFilterService _filterService;
    public UserFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("users"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<UserModel>> HandleAsync([FromQuery] FilterRequest request, CancellationToken ct = default)
    {
        return await _filterService.FilterWithCountAsync<AppUser, UserModel>(request, ct);
    }
}