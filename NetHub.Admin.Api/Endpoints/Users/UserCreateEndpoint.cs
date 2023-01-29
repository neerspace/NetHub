using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Admin.Infrastructure.Models.Users;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Api.Endpoints.Users;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Users)]
[Authorize(Policy = Policies.HasManageUsersPermission)]
public sealed class UserCreateEndpoint : Endpoint<UserCreateRequest, UserModel>
{
    private readonly ISqlServerDatabase _database;
    private readonly UserManager<AppUser> _userManager;

    public UserCreateEndpoint(ISqlServerDatabase database, UserManager<AppUser> userManager)
    {
        _database = database;
        _userManager = userManager;
    }


    [HttpPost("users")]
    public override async Task<UserModel> HandleAsync([FromBody] UserCreateRequest request, CancellationToken ct)
    {
        if (await _database.Set<AppUser>().Where(e => e.NormalizedUserName == request.UserName.ToUpper()).AnyAsync(ct))
            throw new ValidationFailedException($"User with given username '{nameof(UserCreateRequest.UserName)}' already exists");

        var user = request.Adapt<AppUser>();

        var result = string.IsNullOrEmpty(request.Password)
            ? await _userManager.CreateAsync(user)
            : await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new ValidationFailedException("User not created", result.ToErrorDetails());

        // await _userManager.AddToRoleAsync(user, "Admin");

        await _database.SaveChangesAsync(ct);

        return user.Adapt<UserModel>();
    }
}