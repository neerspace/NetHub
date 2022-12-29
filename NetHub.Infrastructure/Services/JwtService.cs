using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Options;
using NetHub.Application.SharedServices;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Infrastructure.Services;

[Service]
public class JwtService : IJwtService
{
    private readonly ISqlServerDatabase _database;
    private readonly JwtOptions _options;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AccessTokenGenerator _accessTokenGenerator;

    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public JwtService(
        ISqlServerDatabase database,
        IOptions<JwtOptions> jwtOptionsAccessor,
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
}