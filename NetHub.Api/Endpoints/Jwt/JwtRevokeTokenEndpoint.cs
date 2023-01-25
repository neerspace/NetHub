using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Options;

namespace NetHub.Api.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtRevokeTokenEndpoint : ActionEndpoint
{
    private readonly JwtOptions _options;
    public JwtRevokeTokenEndpoint(IOptions<JwtOptions> optionsAccessor) => _options = optionsAccessor.Value;


    [HttpPost("auth/revoke-token")]
    public override Task HandleAsync(CancellationToken ct = default)
    {
        Response.Cookies.Delete(_options.RefreshToken.CookieName);
        return Task.CompletedTask;
    }
}