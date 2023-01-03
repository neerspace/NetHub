using Microsoft.AspNetCore.Authentication;
using NeerCore.Api.Extensions;
using NetHub.Api.Shared.Extensions;
using NetHub.Application.Options;
using ExceptionHandlerOptions = NeerCore.Api.ExceptionHandlerOptions;

namespace NetHub.Api;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddNeerApiServices();

        services.Configure<ExceptionHandlerOptions>(o =>
        {
            o.Extended500ExceptionMessage = true;
            o.HandleFluentValidationExceptions = true;
        });

        services.AddCorsPolicy(configuration);

        services.AddNeerControllers();

        services.AddJwtAuthentication().WithGoogleAuthProvider(configuration);
        services.AddPoliciesAuthorization();
    }

    private static void WithGoogleAuthProvider(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        var googleOptions = configuration.GetSection("Google").Get<GoogleOptions>()!;

        builder.AddGoogleOpenIdConnect(options =>
        {
            options.ClientId = googleOptions.ClientId;
            options.ClientSecret = googleOptions.ClientSecret;
        });
    }
}