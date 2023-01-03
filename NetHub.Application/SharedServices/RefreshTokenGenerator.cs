using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using NetHub.Application.Models;
using NetHub.Application.Options;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.SharedServices;

[Service]
public sealed class RefreshTokenGenerator
{
    private readonly JwtOptions _options;
    private readonly ISqlServerDatabase _database;

    public RefreshTokenGenerator(ISqlServerDatabase database, IOptions<JwtOptions> optionsAccessor)
    {
        _database = database;
        _options = optionsAccessor.Value;
    }

    public async Task<JwtToken> GenerateAsync(AppUser user, AppDevice device, CancellationToken ct = default)
    {
        var expires = DateTime.UtcNow.Add(_options.RefreshToken.Lifetime);
        string token = GenerateRandomToken();

        _database.Set<AppToken>().Add(new AppToken
        {
            Value = token,
            Name = TokenNames.Refresh,
            LoginProvider = LoginProviders.NetHub,
            UserId = user.Id,
            Device = device
        });

        await _database.SaveChangesAsync(cancel: ct);

        return new JwtToken(token, expires);
    }

    private static string GenerateRandomToken()
    {
        byte[] randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        // To base64 without ending '=='
        string base64 = Convert.ToBase64String(randomNumber)[..^2];
        return base64.Replace('/', '-').Replace('+', '_');
    }
}