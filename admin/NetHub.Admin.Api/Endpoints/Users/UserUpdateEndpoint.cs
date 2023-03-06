using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Exceptions;
using NetHub.Admin.Models.Users;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Extensions;

namespace NetHub.Admin.Api.Endpoints.Users;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Users)]
[Authorize(Policy = Policies.HasManageUsersPermission)]
public sealed class UserUpdateEndpoint : ActionEndpoint<UserUpdateRequest>
{
    private readonly UserManager<AppUser> _userManager;
    public UserUpdateEndpoint(UserManager<AppUser> userManager) => _userManager = userManager;


    [HttpPut("users")]
    public override async Task HandleAsync([FromBody] UserUpdateRequest request, CancellationToken ct)
    {
        await Task.Delay(2000, ct);
        var user = await _userManager.GetByIdAsync(request.Id);
        if (user is null)
            throw new NotFoundException($"User with Id '{request.Id}' does not exist");

        request.Adapt(user);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new ValidationFailedException("User not updated", result.ToErrorDetails());

        if (!string.IsNullOrEmpty(request.Password))
            await _userManager.AddPasswordAsync(user, request.Password);
    }
}