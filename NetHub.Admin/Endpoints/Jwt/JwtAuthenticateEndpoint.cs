using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Exceptions;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Jwt;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Admin.Endpoints.Jwt;

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
    public override async Task<AuthResult> HandleAsync([FromBody] AuthRequest request, CancellationToken ct = default)
    {
        var user = await _database.Set<AppUser>().GetByLoginAsync(request.Login, ct);
        if (user is null)
            throw new NotFoundException("User not found.");

        return await PasswordAuthorizeAsync(user, request.Password!, ct);
    }

    private async Task<AuthResult> PasswordAuthorizeAsync(AppUser user, string password, CancellationToken cancel)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            throw new ValidationFailedException("Invalid login or password.");

        return await _jwtService.GenerateAsync(user, cancel);
    }
}