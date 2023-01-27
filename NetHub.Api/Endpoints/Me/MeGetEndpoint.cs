using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Extensions;
using NetHub.Application.Models.Users;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public class MeGetEndpoint : ResultEndpoint<UserDto>
{
    private readonly UserManager<AppUser> _userManager;
    public MeGetEndpoint(UserManager<AppUser> userManager) => _userManager = userManager;


    [HttpGet("me")]
    public override async Task<UserDto> HandleAsync(CancellationToken ct = default)
    {
        var userId = UserProvider.UserId;
        var user = await _userManager.FindByIdAsync(userId);

        return user!.Adapt<UserDto>();
    }
}