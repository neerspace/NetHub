using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Jwt;
using NetHub.Application.Interfaces;

namespace NetHub.Admin.Endpoints.Auth;

[Tags(TagNames.Auth)]
[ApiVersion(Versions.V1)]
public class AuthRefreshEndpoint : ActionEndpoint<JwtRefreshRequest>
{
    private readonly IJwtService _jwtService;

    public AuthRefreshEndpoint(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }


    [HttpPost("jwt/refresh")]
    public override async Task HandleAsync([FromBody] JwtRefreshRequest request, CancellationToken ct = default)
    {
        await _jwtService.RefreshAsync(request.RefreshToken, ct);
    }
}