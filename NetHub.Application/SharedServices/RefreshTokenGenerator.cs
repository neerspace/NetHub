using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using NetHub.Application.Extensions;
using NetHub.Application.Models;
using NetHub.Application.Options;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.SharedServices;

[Service]
public sealed class RefreshTokenGenerator
{
    private readonly JwtOptions _options;
    private readonly ISqlServerDatabase _database;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public RefreshTokenGenerator(
        ISqlServerDatabase database, IOptions<JwtOptions> optionsAccessor, IHttpContextAccessor httpContextAccessor)
    {
        _database = database;
        _httpContextAccessor = httpContextAccessor;
        _options = optionsAccessor.Value;
    }

    public async Task<JwtToken> GenerateAsync(AppUser user, CancellationToken cancel = default)
    {
        var expires = DateTime.UtcNow.Add(_options.RefreshTokenLifetime);
        string token = GenerateRandomToken();

        var userAgent = HttpContext.GetUserAgent();

        _database.Set<AppToken>().Add(new AppToken
        {
            Value = user.Id + ":" + token,
            UserId = user.Id,
            Ip = HttpContext.GetIPAddress().ToString(),
            Device = userAgent.Platform,
            Browser = userAgent.Browser,
            BrowserVersion = userAgent.BrowserVersion,
        });

        await _database.SaveChangesAsync(cancel: cancel);

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