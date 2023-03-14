using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Me;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public sealed class MeProfileUsernameUpdateEndpoint: ActionEndpoint<MeProfileUsernameUpdateRequest>
{
    private readonly UserManager<AppUser> _userManager;

    public MeProfileUsernameUpdateEndpoint(UserManager<AppUser> userManager) => _userManager = userManager;

    [HttpPut("me/profile-username"), ClientSide(ActionName = "updateProfileUsername")]
    public override async Task HandleAsync(MeProfileUsernameUpdateRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.Equals(user.UserName, request.Username, StringComparison.CurrentCultureIgnoreCase))
            return;

        //TODO: Add Username regex

        // if ()
        // {
            // throw new ValidationFailedException("username", "You already")
        // }

        bool isExist = await Database.Set<AppUser>().AnyAsync(u => u.UserName == request.Username, ct);

        if (isExist)
            throw new ValidationFailedException("username", "User with such username already exists");

        await _userManager.SetUserNameAsync(user, request.Username);

        await Database.SaveChangesAsync(ct);
    }
}