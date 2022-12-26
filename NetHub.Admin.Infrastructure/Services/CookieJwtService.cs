using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Admin.Infrastructure.Options;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Options;
using NetHub.Application.Services;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Infrastructure.Services;

[Service]
public class CookieJwtService : IJwtService
{
    private readonly ISqlServerDatabase _database;
    private readonly CookieJwtOptions _options;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AccessTokenGenerator _accessTokenGenerator;

    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public CookieJwtService(
        ISqlServerDatabase database,
        IOptions<CookieJwtOptions> jwtOptionsAccessor,
        AccessTokenGenerator accessTokenGenerator,
        RefreshTokenGenerator refreshTokenGenerator,
        IHttpContextAccessor httpContextAccessor)
    {
        _database = database;
        _options = jwtOptionsAccessor.Value;
        _refreshTokenGenerator = refreshTokenGenerator;
        _httpContextAccessor = httpContextAccessor;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<AuthResult> GenerateAsync(AppUser user, CancellationToken ct)
    {
        (string? accessToken, DateTime accessTokenExpires) = await _accessTokenGenerator.GenerateAsync(user, ct);
        (string? refreshToken, DateTime refreshTokenExpires) = await _refreshTokenGenerator.GenerateAsync(user, ct);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = accessTokenExpires,
        };
        HttpContext.Response.Cookies.Append(_options.AccessToken.CookieName, accessToken, cookieOptions);
        HttpContext.Response.Cookies.Append(_options.RefreshToken.CookieName, refreshToken, cookieOptions);

        return new AuthResult
        {
            Username = user.UserName,
            FirstName = $"{user.FirstName} {user.LastName}".Trim(),
            Token = accessToken,
            TokenExpires = accessTokenExpires,
            RefreshToken = refreshToken,
            RefreshTokenExpires = refreshTokenExpires
        };
    }

    public async Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct)
    {
        var refreshTokens = _database.Set<RefreshToken>();

        var token = await refreshTokens
            .Where(rt => rt.Value == refreshToken)
            .Include(rt => rt.User)
            .FirstOr404Async(ct);

        if (!_refreshTokenGenerator.IsValid(token))
            throw new ValidationFailedException("Refresh token is invalid.");

        var result = await GenerateAsync(token.User!, ct);
        token.User = null;

        refreshTokens.Remove(token);
        await _database.SaveChangesAsync(cancel: ct);

        return result;
    }
}