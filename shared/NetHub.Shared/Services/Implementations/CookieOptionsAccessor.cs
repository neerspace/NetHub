using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using NetHub.Shared.Options;

namespace NetHub.Shared.Services.Implementations;

[Service]
public class CookieOptionsAccessor
{
    private readonly JwtOptions _options;

    public CookieOptionsAccessor(IOptions<JwtOptions> optionsAccessor) => _options = optionsAccessor.Value;

    public CookieOptions GetRefreshOptions(DateTimeOffset? expires = null)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            Domain = _options.RefreshToken.CookieDomain,
        };

        if (expires is null)
            return cookieOptions;

        cookieOptions.Expires = expires;

        return cookieOptions;
    }
}