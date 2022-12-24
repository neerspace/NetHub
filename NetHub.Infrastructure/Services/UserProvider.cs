using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Extensions;
using NetHub.Application.Interfaces;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Infrastructure.Services;

[Service]
internal sealed class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _accessor;

    private UserManager<User> UserManager =>
        _accessor.HttpContext!.RequestServices.GetRequiredService<UserManager<User>>();


    private User? _userProfile;

    public UserProvider(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public ClaimsPrincipal User => _accessor.HttpContext!.User;

    public long GetUserId() => User.GetUserId();

    public long? TryGetUserId()
    {
        var claimResult = User.TryGetClaimWithoutAuthorization(Claims.Id, out Claim? claim);
        if (!claimResult) return null;

        var longResult = long.TryParse(claim?.Value, out long userId);

        return longResult ? userId : null;
    }


    public async Task<User> GetUser()
    {
        var user = await UserManager.FindByIdAsync(GetUserId().ToString());
        if (user is null) throw new UnauthorizedException("Authorized used required");

        return _userProfile ??= user;
    }
}