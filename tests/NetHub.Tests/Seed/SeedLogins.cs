using Microsoft.AspNetCore.Identity;
using NetHub.Application.Features.Public.Users.Sso;

namespace NetHub.Tests.Seed;

public static class SeedLogins
{
    public static string DoesntExistTelegramId = "123456789";

    private static long GeekId = 1;
    private static long BgId = 2;
    public static string GeekMail = "geektweeker@gmail.com";
    public static string BgMail = "bogdana@gmail.com";
    public static string TelegramId123 = "123";
    public static string TelegramId456 = "456";

    public static readonly IdentityUserLogin<long>[] Default =
    {
        new()
        {
            UserId = GeekId,
            ProviderDisplayName = ProviderType.Google.ToString().ToLower(),
            LoginProvider = ProviderType.Google.ToString().ToLower(),
            ProviderKey = GeekMail
        }
    };
}