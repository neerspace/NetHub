using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public class MeGetEndpoint : ResultEndpoint<UserResult>
{
    private readonly UserManager<AppUser> _userManager;
    public MeGetEndpoint(UserManager<AppUser> userManager) => _userManager = userManager;


    [HttpGet("me"), ClientSide(ActionName = "me")]
    public override async Task<UserResult> HandleAsync(CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        var user = await _userManager.GetByIdAsync(userId);

        return user!.Adapt<UserResult>();
    }
}