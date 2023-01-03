using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Extensions;
using NetHub.Application.Models;
using NetHub.Application.Options;
using NetHub.Core.Enums;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.SharedServices;

[Service]
public sealed class RefreshTokenGenerator
{
    private const string InvalidPlatform = "Unknown Platform";

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

    public async Task<JwtToken> GenerateAsync(AppUser user, CancellationToken ct = default)
    {
        var expires = DateTime.UtcNow.Add(_options.RefreshToken.Lifetime);
        string token = GenerateRandomToken();

        var userAgent = HttpContext.GetUserAgent();
        var ip = HttpContext.GetIPAddress().ToString();

        if (userAgent.IsRobot // coz we hate robots here
            // UA platform is invalid
            || string.Equals(userAgent.Platform, InvalidPlatform, StringComparison.OrdinalIgnoreCase)
            // UA browser is invalid
            || string.IsNullOrEmpty(userAgent.Browser)
            || string.IsNullOrEmpty(userAgent.BrowserVersion))
            throw new ForbidException("You are using a suspicious device.\n"
                + "Make sure you are using a modern browser and do not use a toaster to surf the web.");

        var device = await _database.Set<AppDevice>().AsNoTracking()
            .FirstOrDefaultAsync(d => d.Browser == userAgent.Browser && d.IpAddress == ip, ct);

        if (device?.Status == DeviceStatus.Banned)
            throw new ForbidException("Your IP has been blocked.\n"
                + "Contact admins to unlock access for your IP.");

        _database.Set<AppToken>().Add(new AppToken
        {
            Value = token,
            UserId = user.Id,
            Device = device
                ?? new AppDevice
                {
                    IpAddress = ip,
                    Browser = userAgent.Browser,
                    BrowserVersion = userAgent.BrowserVersion,
                }
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