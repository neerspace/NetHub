using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Me;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me;

public class MeProfileUpdateEndpoint : ActionEndpoint<MeProfileUpdateRequest>
{
    private readonly UserManager<AppUser> _userManager;

    public MeProfileUpdateEndpoint(UserManager<AppUser> userManager) => _userManager = userManager;

    [HttpPut("me/profile"), ClientSide(ActionName = "updateProfile")]

    public override async Task HandleAsync(MeProfileUpdateRequest request, CancellationToken ct)
    {
        var user = await Database.Set<AppUser>().FirstOr404Async(u => u.Id == UserProvider.UserId, cancel: ct);

        if (!string.IsNullOrWhiteSpace(request.Username))
            await UpdateUsername(request.Username);

        if (!string.IsNullOrWhiteSpace(request.FirstName) && user.FirstName != request.FirstName)
        {
            user.FirstName = request.FirstName;
            user.NormalizedUserName = request.FirstName.ToUpper();
        }

        if (!string.IsNullOrWhiteSpace(request.LastName) && user.LastName != request.LastName)
            user.LastName = request.LastName;

        if (user.MiddleName != request.MiddleName)
            user.MiddleName = request.MiddleName;

        if (user.Description != request.Description)
            user.Description = request.Description;

        await Database.SaveChangesAsync(ct);
    }



    private async Task UpdateUsername(string username)
    {
        //TODO: Add Username regex

        var isExist = await Database.Set<AppUser>().AnyAsync(u => u.UserName == username);

        if (isExist)
            throw new ValidationFailedException("username", "User with such username already exists");

        var user = await UserProvider.GetUserAsync();

        await _userManager.SetUserNameAsync(user, username);

        await Database.SaveChangesAsync();
    }
}