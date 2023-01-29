using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Api.Shared.Extensions;
using NetHub.Application.Options;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;
using Swashbuckle.AspNetCore.SwaggerGen;
using ExceptionHandlerOptions = NeerCore.Api.ExceptionHandlerOptions;

namespace NetHub.Api;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpClient();

        services.Configure<SwaggerGenOptions>(options =>
        {
            // options.CustomOperationIds(NSwagEndpointNameFactory.Create);
            // options.DocumentFilter<ResponsesFilter>();
            options.EnableAnnotations();
            options.SupportNonNullableReferenceTypes();
        });

        services.AddNeerApiServices();

        services.Configure<ExceptionHandlerOptions>(o =>
            o.Extended500ExceptionMessage = environment.IsDevelopment());

        services.AddNeerControllers();
        // .AddMvcOptions(options => options.Filters.Add<SuccessStatusCodesFilter>());
        services.AddCorsPolicy(configuration);

        services.ConfigureAllOptions();

        services.AddJwtAuthentication().WithGoogleAuthProvider(configuration);
        services.AddAuthorization();

        services.AddFluentValidation();
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

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(fv =>
            fv.DisableDataAnnotationsValidation = true);
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<AppUser>(ServiceLifetime.Transient);
        services.AddFluentValidationRulesToSwagger();
    }
}