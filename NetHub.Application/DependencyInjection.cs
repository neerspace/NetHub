using System.Reflection;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.Application.Extensions;
using NetHub.Application.Options;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterMappings();
        services.AddMediatorApplication();
        services.ConfigureOptions(configuration);
        services.AddTransient<SignInManager<AppUser>>();
    }


    public static void ConfigureFluentValidation(FluentValidationMvcConfiguration options)
    {
        options.DisableDataAnnotationsValidation = true;
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
    }

    private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: use NeerCore Configurator instead
        services.Configure<MezhaOptions>(configuration.GetSection("Mezha"));
        services.Configure<TelegramOptions>(configuration.GetSection("Telegram"));
        services.Configure<FacebookOptions>(configuration.GetSection("Facebook"));
        services.ConfigureOptions<JwtOptions.Configurator>();
        services.Configure<CurrencyRateOptions>(configuration.GetSection("CurrencyRate"));
    }

    private static void RegisterMappings(this IServiceCollection services)
    {
        var register = new MappingRegister();

        register.Register(TypeAdapterConfig.GlobalSettings);
    }

    private static void AddCustomMediatR(this IServiceCollection services)
    {
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        services.AddMediatR(assemblies).AddFluentValidation(ConfigureFluentValidation);
        services.AddFluentValidation(assemblies);
    }
}