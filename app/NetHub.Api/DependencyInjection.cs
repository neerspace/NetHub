using Microsoft.AspNetCore.Authentication;
using NetHub.Core.Constants;
using NetHub.Shared.Api.Extensions;
using NetHub.Shared.Options;

namespace NetHub.Api;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddJwtAuthentication().WithGoogleAuthProvider(configuration);
        services.AddAuthorization();
    }

    private static void WithGoogleAuthProvider(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        var googleOptions = configuration.GetSection(ConfigSectionNames.Google).Get<GoogleOptions>()!;

        builder.AddGoogleOpenIdConnect(options =>
        {
            options.ClientId = googleOptions.ClientId;
            options.ClientSecret = googleOptions.ClientSecret;
        });
    }
}