using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetHub.Admin.Abstractions;
using NetHub.Application.Options;

namespace NetHub.Admin.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtRevokeTokenEndpoint : ActionEndpoint
{
    private readonly JwtOptions _options;

    public JwtRevokeTokenEndpoint(IOptions<JwtOptions> optionsAccessor)
    {
        _options = optionsAccessor.Value;
    }


    [HttpPost("auth/revoke-token")]
    public override Task HandleAsync(CancellationToken ct = default)
    {
        Response.Cookies.Delete(_options.RefreshTokenCookieName);
        return Task.CompletedTask;
    }
}