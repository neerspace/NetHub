using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Options;
using NetHub.Shared.Services.Implementations;

namespace NetHub.Admin.Api.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtRevokeTokenEndpoint : ActionEndpoint
{
    private readonly JwtOptions _options;
    private readonly CookieOptionsAccessor _cookieOptionsAccessor;
    public JwtRevokeTokenEndpoint(IOptions<JwtOptions> optionsAccessor, CookieOptionsAccessor cookieOptionsAccessor)
    {
        _options = optionsAccessor.Value;
        _cookieOptionsAccessor = cookieOptionsAccessor;
    }


    [HttpPost("auth/revoke-token")]
    public override Task HandleAsync(CancellationToken ct)
    {
        Response.Cookies.Delete(_options.RefreshToken.CookieName, _cookieOptionsAccessor.GetRefreshOptions());
        return Task.CompletedTask;
    }
}