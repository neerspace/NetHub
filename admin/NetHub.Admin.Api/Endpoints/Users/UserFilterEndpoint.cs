using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Models.Users;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Users;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Users)]
[Authorize(Policy = Policies.HasReadUsersPermission)]
public sealed class UserFilterEndpoint : FilterEndpoint<FilterRequest, UserModel>
{
    private readonly IFilterService _filterService;
    public UserFilterEndpoint(IFilterService filterService) => _filterService = filterService;


    [HttpGet("users"), ClientSide(ActionName = "filter")]
    public override async Task<Filtered<UserModel>> HandleAsync([FromQuery] FilterRequest request, CancellationToken ct)
    {
        return await _filterService.FilterWithCountAsync<AppUser, UserModel>(request, ct);
    }
}