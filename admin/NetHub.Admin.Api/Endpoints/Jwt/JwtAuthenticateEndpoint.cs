using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Exceptions;
using NetHub.Admin.Models.Jwt;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;
using NetHub.Shared.Models.Jwt;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtAuthenticateEndpoint : Endpoint<AuthRequest, AuthResult>
{
    private readonly IJwtService _jwtService;
    private readonly ISqlServerDatabase _database;
    private readonly SignInManager<AppUser> _signInManager;

    public JwtAuthenticateEndpoint(ISqlServerDatabase database, IJwtService jwtService, SignInManager<AppUser> signInManager)
    {
        _database = database;
        _jwtService = jwtService;
        _signInManager = signInManager;
    }


    [HttpPost("jwt/authenticate")]
    public override async Task<AuthResult> HandleAsync([FromBody] AuthRequest request, CancellationToken ct)
    {
        var user = await _database.Set<AppUser>().GetByLoginAsync(request.Login, ct);
        if (user is null)
            throw new ValidationFailedException("login", "Invalid login or password");

        return await PasswordAuthorizeAsync(user, request.Password!, ct);
    }

    private async Task<AuthResult> PasswordAuthorizeAsync(AppUser user, string password, CancellationToken cancel)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            throw new ValidationFailedException("login", "Invalid login or password");

        return await _jwtService.GenerateAsync(user, cancel);
    }
}