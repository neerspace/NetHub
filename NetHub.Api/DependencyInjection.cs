using Microsoft.AspNetCore.Authentication;
using NeerCore.Api.Extensions;
using NetHub.Api.Shared.Extensions;
using NetHub.Application.Options;
using NetHub.Core.Constants;
using ExceptionHandlerOptions = NeerCore.Api.ExceptionHandlerOptions;

namespace NetHub.Api;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpClient();

        services.AddNeerApiServices();

        services.Configure<ExceptionHandlerOptions>(o =>
            o.Extended500ExceptionMessage = environment.IsDevelopment());

        services.AddCorsPolicy(configuration);

        services.AddNeerControllers();

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