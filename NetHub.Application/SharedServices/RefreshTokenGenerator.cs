using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using NetHub.Application.Models;
using NetHub.Application.Options;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.SharedServices;

[Service]
public sealed class RefreshTokenGenerator
{
    private readonly JwtOptions _options;
    private readonly ISqlServerDatabase _database;
    private readonly DbSet<RefreshToken> _refreshTokensSet;

    public RefreshTokenGenerator(IOptions<JwtOptions> optionsAccessor, ISqlServerDatabase database)
    {
        _database = database;
        _options = optionsAccessor.Value;
        _refreshTokensSet = _database.Set<RefreshToken>();
    }

    public async Task<JwtToken> GenerateAsync(AppUser user, CancellationToken cancel = default)
    {
        DateTime expires = DateTime.UtcNow.Add(_options.RefreshTokenLifetime);
        string token = GenerateRandomToken();

        _refreshTokensSet.Add(new RefreshToken
        {
            Value = token,
            UserId = user.Id
        });

        await _database.SaveChangesAsync(cancel: cancel);

        return new JwtToken(token, expires);
    }

    public bool IsValid(RefreshToken token)
    {
        return !string.IsNullOrEmpty(token.Value)
               && token.Created.Add(_options.RefreshTokenLifetime) > DateTime.UtcNow;
    }

    private static string GenerateRandomToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        // To base64 without ending '=='
        string base64 = Convert.ToBase64String(randomNumber)[..^2];
        return base64.Replace('/', '-').Replace('+', '_');
    }
}