using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NeerCore.Exceptions;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Jwt;
using NetHub.Shared.Options;
using NetHub.Shared.Services;

namespace NetHub.Api.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtRefreshTokenEndpoint : ResultEndpoint<JwtResult>
{
    private readonly IJwtService _jwtService;
    private readonly JwtOptions _jwtOptions;

    public JwtRefreshTokenEndpoint(IJwtService jwtService, IOptions<JwtOptions> jwtOptionsAccessor)
    {
        _jwtService = jwtService;
        _jwtOptions = jwtOptionsAccessor.Value;
    }


    [HttpPost("jwt/refresh")]
    public override async Task<JwtResult> HandleAsync(CancellationToken ct)
    {
        if (HttpContext.Request.Cookies.TryGetValue(_jwtOptions.RefreshToken.CookieName, out var refreshToken))
            return await _jwtService.RefreshAsync(refreshToken, ct);

        throw new UnauthorizedException("Refresh token does not exist");
    }
}