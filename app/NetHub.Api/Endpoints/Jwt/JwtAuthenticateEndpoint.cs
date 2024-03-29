using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Extensions;
using NetHub.Shared.Models.Jwt;
using NetHub.Shared.Services;

namespace NetHub.Api.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtAuthenticateEndpoint : Endpoint<JwtAuthenticateRequest, JwtResult>
{
    private readonly ISqlServerDatabase _database;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAuthValidator _validator;
    private readonly IJwtService _jwtService;

    public JwtAuthenticateEndpoint(
        UserManager<AppUser> userManager, ISqlServerDatabase database,
        IAuthValidator validator, IJwtService jwtService)
    {
        _database = database;
        _userManager = userManager;
        _validator = validator;
        _jwtService = jwtService;
    }


    [HttpPost("jwt/authenticate")]
    public override async Task<JwtResult> HandleAsync([FromBody] JwtAuthenticateRequest request, CancellationToken ct)
    {
        // try to get provider login info
        var loginInfo = await GetUserLoginInfoAsync(request.ProviderKey, request.Provider, ct);

        // if info exists, try to get user
        var user = loginInfo is null ? null : await _userManager.FindByIdAsync(loginInfo.UserId.ToString());

        if (user is null)
            // is user is null - register
            user = await RegisterUserAsync(request, ct);
        else
            // else - just validate
            await ValidateUserAsync(request, ct);

        // if validation passed - generate and return tokens
        return await _jwtService.GenerateAsync(user, ct);
    }

    private async Task<AppUser> RegisterUserAsync(JwtAuthenticateRequest request, CancellationToken ct)
    {
        await ValidateUserAsync(request, ct);

        var user = new AppUser
        {
            UserName = Truncate(request.Username.ToLower()),
            FirstName = request.FirstName!,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email!,
            ProfilePhotoUrl = request.ProfilePhotoUrl,
            EmailConfirmed = request.Provider is not ProviderType.Telegram,
            UsernameChanges = new() { Count = 0 }
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
            throw new ValidationFailedException("User not created", result.ToErrorDetails());

        await _userManager.AddLoginAsync(user,
            new UserLoginInfo(request.Provider.ToString().ToLower(),
                request.ProviderKey,
                null));

        return user;
    }

    private async Task ValidateUserAsync(JwtAuthenticateRequest request, CancellationToken ct)
    {
        if (request.ProviderMetadata is not { Count: > 0 })
            throw new ValidationFailedException("Metadata not provided");

        bool isValid = await _validator.ValidateAsync(request, ct);

        if (!isValid)
            throw new ValidationFailedException("Provided invalid data");
    }

    private async Task<IdentityUserLogin<long>?> GetUserLoginInfoAsync(string key, ProviderType provider, CancellationToken ct)
    {
        string requestLoginProvider = provider.ToString().ToLower();
        return await _database.Set<AppUserLogin>()
            .SingleOrDefaultAsync(info =>
                info.ProviderKey == key && info.LoginProvider == requestLoginProvider, ct);
    }

    private string Truncate(string username) => username.Length > 12 ? username[..12] : username;
}