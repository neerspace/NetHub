using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Exceptions;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models;
using NetHub.Admin.Infrastructure.Models.Jwt;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Admin.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtAuthorizeEndpoint : Endpoint<JwtAuthRequest, AdminAuthResult>
{
    private readonly IJwtService _jwtService;
    private readonly ISqlServerDatabase _database;
    private readonly SignInManager<AppUser> _signInManager;

    public JwtAuthorizeEndpoint(ISqlServerDatabase database, IJwtService jwtService, SignInManager<AppUser> signInManager)
    {
        _database = database;
        _jwtService = jwtService;
        _signInManager = signInManager;
    }

    [HttpPost("jwt/authorize")]
    public override async Task<AdminAuthResult> HandleAsync([FromBody] JwtAuthRequest request, CancellationToken ct = default)
    {
        var user = await _database.Set<AppUser>().GetByLoginAsync(request.Login, ct);
        if (user is null) throw new NotFoundException("User not found.");

        var jwt = await PasswordAuthorizeAsync(user, request.Password!, ct);

        return new AdminAuthResult
        {
            Name = jwt.FirstName,
            Username = jwt.Username,
            ProfilePhotoUrl = jwt.ProfilePhotoLink,
        };
    }

    private async Task<AuthResult> PasswordAuthorizeAsync(AppUser user, string password, CancellationToken cancel)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) throw new ValidationFailedException("Invalid login or password.");

        return await _jwtService.GenerateAsync(user, cancel);
    }
}