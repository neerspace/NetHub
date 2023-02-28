using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Options;
using NetHub.Shared.Services.Implementations;

namespace NetHub.Api.Endpoints.Jwt;

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


    [HttpDelete("auth/revoke")]
    public override Task HandleAsync(CancellationToken ct)
    {
        Response.Cookies.Delete(_options.RefreshToken.CookieName, _cookieOptionsAccessor.GetRefreshOptions());
        return Task.CompletedTask;
    }
}