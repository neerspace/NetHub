using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Options;

namespace NetHub.Api.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtRevokeTokenEndpoint : ActionEndpoint
{
    private readonly JwtOptions _options;
    public JwtRevokeTokenEndpoint(IOptions<JwtOptions> optionsAccessor) => _options = optionsAccessor.Value;


    [HttpDelete("auth/revoke")]
    public override Task HandleAsync(CancellationToken ct)
    {
        Response.Cookies.Delete(_options.RefreshToken.CookieName, new()
        {
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            // SameSite = _environment.IsProduction() ? SameSiteMode.Strict : SameSiteMode.Lax,
            Domain = _options.RefreshToken.CookieDomain,
        });
        return Task.CompletedTask;
    }
}