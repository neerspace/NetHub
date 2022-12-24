using Microsoft.EntityFrameworkCore;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Infrastructure.Services.Internal;
using NetHub.Data.SqlServer.Extensions;


namespace NetHub.Infrastructure.Services;

[Service]
internal sealed class JwtService : IJwtService
{
    private readonly ISqlServerDatabase _database;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;
    private readonly AccessTokenGenerator _accessTokenGenerator;

    public JwtService(ISqlServerDatabase database, AccessTokenGenerator accessTokenGenerator,
        RefreshTokenGenerator refreshTokenGenerator)
    {
        _database = database;
        _refreshTokenGenerator = refreshTokenGenerator;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<AuthResult> GenerateAsync(User user, CancellationToken cancel)
    {
        (string? accessToken, DateTime accessTokenExpires) = await _accessTokenGenerator.GenerateAsync(user, cancel);
        (string? refreshToken, DateTime refreshTokenExpires) = await _refreshTokenGenerator.GenerateAsync(user, cancel);

        return new AuthResult
        {
            Username = user.UserName,
            Token = accessToken,
            TokenExpires = accessTokenExpires,
            RefreshToken = refreshToken,
            RefreshTokenExpires = refreshTokenExpires
        };
    }

    public async Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken cancel)
    {
        var refreshTokens = _database.Set<RefreshToken>();

        var token = await refreshTokens.Where(rt => rt.Value == refreshToken).Include(rt => rt.User)
            .FirstOr404Async(cancel);
        if (!_refreshTokenGenerator.IsValid(token))
            throw new ValidationFailedException("Refresh token is invalid.");

        var result = await GenerateAsync(token.User!, cancel);
        token.User = null;

        refreshTokens.Remove(token);
        await _database.SaveChangesAsync(cancel: cancel);

        return result;
    }
}