using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Extensions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Options;
using NetHub.Application.SharedServices;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Infrastructure.Services;

[Service]
public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _options;
    private readonly ISqlServerDatabase _database;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AccessTokenGenerator _accessTokenGenerator;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;

    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public JwtService(
        ISqlServerDatabase database, IOptions<JwtOptions> jwtOptionsAccessor, IHttpContextAccessor httpContextAccessor,
        AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator)
    {
        _database = database;
        _options = jwtOptionsAccessor.Value;
        _httpContextAccessor = httpContextAccessor;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }


    public async Task<AuthResult> GenerateAsync(AppUser user, CancellationToken ct)
    {
        (string? accessToken, DateTime accessTokenExpires) = await _accessTokenGenerator.GenerateAsync(user, ct);
        (string? refreshToken, DateTime refreshTokenExpires) = await _refreshTokenGenerator.GenerateAsync(user, ct);

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

        var token = await refreshTokens
            .Where(rt => rt.Value == refreshToken)
            .Include(rt => rt.User).Include(rt => rt.Device)
            .FirstOr404Async(ct);

        if (!IsRefreshTokenValid(token))
            throw new ForbidException("Provided token is not a valid refresh token.");

        var result = await GenerateAsync(token.User!, ct);
        token.User = null;

        refreshTokens.Remove(token);
        await _database.SaveChangesAsync(cancel: ct);

        return result;
    }

    private void SetRefreshTokenCookie(string refreshToken, DateTime refreshTokenExpires) =>
        HttpContext.Response.Cookies.Append(_options.RefreshTokenCookieName, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            Domain = "localhost",
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
        && refreshToken.Created.Add(_options.RefreshTokenLifetime) < DateTime.UtcNow
        // token was provided for current request device IP and browser
        && IsDeviceFromCurrentRequest(refreshToken.Device);

    private bool IsDeviceFromCurrentRequest(AppDevice device)
    {
        var userAgent = HttpContext.GetUserAgent();
        return (!_options.RequireSameUserAgent
                || !userAgent.IsRobot // coz we have robots... jk if you're a robot ;)
                && string.Equals(device.Platform, userAgent.Platform, StringComparison.OrdinalIgnoreCase)
                && string.Equals(device.Browser, userAgent.Browser, StringComparison.OrdinalIgnoreCase))
            // token was provided for current request IP
            && (!_options.RequireSameIPAddress
                || string.Equals(HttpContext.GetIPAddress().ToString(), device.IpAddress, StringComparison.OrdinalIgnoreCase));
    }
}