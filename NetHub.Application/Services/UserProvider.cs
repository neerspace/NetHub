using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Extensions;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Services;

[Service]
internal sealed class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _accessor;
    private AppUser? _userProfile;

    private UserManager<AppUser> UserManager => _accessor.HttpContext!.RequestServices.GetRequiredService<UserManager<AppUser>>();

    public UserProvider(IHttpContextAccessor accessor) => _accessor = accessor;

    private ClaimsPrincipal User => _accessor.HttpContext!.User;

    public long UserId => User.GetUserId();
    public string UserName => User.GetUsername();

    public long? TryGetUserId()
    {
        var claimResult = User.TryGetClaimWithoutAuthorization(Claims.Id, out var claim);
        if (!claimResult)
            return null;

        var longResult = long.TryParse(claim?.Value, out long userId);

        return longResult ? userId : null;
    }


    public async Task<AppUser> GetUserAsync()
    {
        var user = await UserManager.FindByIdAsync(UserId.ToString());
        if (user is null)
            throw new UnauthorizedException("Authorized used required");

        return _userProfile ??= user;
    }
}