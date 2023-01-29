using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Options;
using NetHub.Application.SharedServices;
using NetHub.Core.Constants;
using NetHub.Core.Enums;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Infrastructure.Extensions;

namespace NetHub.Infrastructure.Services;

[Service]
public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _options;
    private readonly ISqlServerDatabase _database;
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AccessTokenGenerator _accessTokenGenerator;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;

    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public JwtService(
        ISqlServerDatabase database, IOptions<JwtOptions> jwtOptionsAccessor,
        IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment,
        AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator)
    {
        _database = database;
        _environment = environment;
        _options = jwtOptionsAccessor.Value;
        _httpContextAccessor = httpContextAccessor;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }


    public async Task<AuthResult> GenerateAsync(AppUser user, CancellationToken ct)
    {
        var device = await GetUserDeviceAsync(ct);

        var (accessToken, accessTokenExpires) = await _accessTokenGenerator.GenerateAsync(user, ct);
        var (refreshToken, refreshTokenExpires) = await _refreshTokenGenerator.GenerateAsync(user, device, ct);

        SetRefreshTokenCookie(refreshToken, refreshTokenExpires);

        return new AuthResult
        {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePhotoUrl = user.ProfilePhotoUrl,
            Token = accessToken,
            TokenExpires = accessTokenExpires,
            RefreshTokenExpires = refreshTokenExpires
        };
    }

    public async Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct)
    {
        var refreshTokens = _database.Set<AppToken>();

        var token = await refreshTokens.AsNoTracking()
            .Where(rt => rt.Value == refreshToken)
            .Include(rt => rt.User).Include(rt => rt.Device)
            .FirstOr404Async(ct);

        if (!IsRefreshTokenValid(token))
            throw new ForbidException("Provided token is not a valid refresh token");

        var result = await GenerateAsync(token.User!, ct);
        token.User = null;

        refreshTokens.Remove(token);
        await _database.SaveChangesAsync(cancel: ct);

        return result;
    }

    private void SetRefreshTokenCookie(string refreshToken, DateTimeOffset refreshTokenExpires) =>
        HttpContext.Response.Cookies.Append(_options.RefreshToken.CookieName, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = _environment.IsProduction(),
            IsEssential = true,
            SameSite = _environment.IsProduction() ? SameSiteMode.Strict : SameSiteMode.Lax,
            Domain = _options.RefreshToken.CookieDomain,
            Expires = refreshTokenExpires,
        });

    /// <summary>
    /// Verifies given refresh token by current user request client.
    /// Returns <b>false</b>, if token is not pass verification,
    /// otherwise <b>true</b>.
    /// </summary>
    private bool IsRefreshTokenValid(AppToken? refreshToken) =>
        // if provided token exists (not null)
        refreshToken is not null
        // if provided token is a refresh token
        && refreshToken.Name == TokenNames.Refresh
        // token is not expired yet
        && refreshToken.Created.Add(_options.RefreshToken.Lifetime) > DateTimeOffset.UtcNow
        // token was provided for current request device IP and browser
        && IsDeviceFromCurrentRequest(refreshToken.Device!);

    private bool IsDeviceFromCurrentRequest(AppDevice device)
    {
        var userAgent = HttpContext.GetUserAgent();
        return (!_options.RefreshToken.RequireSameUserAgent
                || !userAgent.IsRobot // coz we have robots... jk if you're a robot ;)
                && string.Equals(device.Platform, userAgent.Platform, StringComparison.OrdinalIgnoreCase)
                && string.Equals(device.Browser, userAgent.Browser, StringComparison.OrdinalIgnoreCase))
            // token was provided for current request IP
            && (!_options.RefreshToken.RequireSameIPAddress
                || string.Equals(HttpContext.GetIPAddress().ToString(), device.IpAddress, StringComparison.OrdinalIgnoreCase));
    }

    private async Task<AppDevice> GetUserDeviceAsync(CancellationToken ct)
    {
        var userAgent = HttpContext.GetUserAgent();
        var ip = HttpContext.GetIPAddress().ToString();

        if (userAgent.IsRobot // coz we hate robots here
            // UA platform is invalid
            || string.Equals(userAgent.Platform, "Unknown Platform", StringComparison.OrdinalIgnoreCase)
            // UA browser is invalid
            || string.IsNullOrEmpty(userAgent.Browser)
            || string.IsNullOrEmpty(userAgent.BrowserVersion))
            throw new ForbidException("You are using a suspicious device.\n"
                + "Make sure you are using a modern browser and do not use a toaster to surf the web");

        var device = await _database.Set<AppDevice>().AsNoTracking()
            .FirstOrDefaultAsync(d => d.Browser == userAgent.Browser && d.IpAddress == ip, ct);

        if (device?.Status == DeviceStatus.Banned)
            throw new ForbidException("Your IP has been blocked.\n"
                + "Contact admins to unlock access for your IP");

        if (device is not null)
            return device;

        device = new AppDevice
        {
            IpAddress = ip,
            Platform = userAgent.Platform,
            Browser = userAgent.Browser,
            BrowserVersion = userAgent.BrowserVersion,
        };

        _database.Set<AppDevice>().Add(device);
        await _database.SaveChangesAsync(ct);

        return device;
    }
}