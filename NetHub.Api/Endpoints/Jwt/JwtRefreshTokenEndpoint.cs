using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NeerCore.Exceptions;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Models.Jwt;
using NetHub.Application.Options;
using NetHub.Application.Services;

namespace NetHub.Api.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtRefreshTokenEndpoint : ResultEndpoint<AuthResult>
{
    private readonly IJwtService _jwtService;
    private readonly JwtOptions _jwtOptions;

    public JwtRefreshTokenEndpoint(IJwtService jwtService, IOptions<JwtOptions> jwtOptionsAccessor)
    {
        _jwtService = jwtService;
        _jwtOptions = jwtOptionsAccessor.Value;
    }


    [HttpPost("jwt/refresh")]
    public override async Task<AuthResult> HandleAsync(CancellationToken ct)
    {
        if (HttpContext.Request.Cookies.TryGetValue(_jwtOptions.RefreshToken.CookieName, out var refreshToken))
            return await _jwtService.RefreshAsync(refreshToken, ct);

        throw new UnauthorizedException("Refresh token does not exist");
    }
}