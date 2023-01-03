using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Options;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;
using Sieve.Models;

namespace NetHub.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterMappings();
        services.AddCustomMediatR();
        services.ConfigureOptions(configuration);
        services.AddTransient<SignInManager<AppUser>>();
    }


    public static void ConfigureFluentValidation(FluentValidationMvcConfiguration options)
    {
        options.DisableDataAnnotationsValidation = true;
        // TODO: Review deprecated features
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
    }

    private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: use NeerCore Configurator instead
        services.Configure<MezhaOptions>(configuration.GetSection(ConfigSectionNames.Mezha));
        services.Configure<TelegramOptions>(configuration.GetSection(ConfigSectionNames.Telegram));
        services.Configure<FacebookOptions>(configuration.GetSection(ConfigSectionNames.Facebook));
        services.ConfigureOptions<JwtOptions.Configurator>();
        services.Configure<CurrencyRateOptions>(configuration.GetSection(ConfigSectionNames.CurrencyRate));
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

        var types = Assembly.GetExecutingAssembly().GetTypes();
        new AssemblyScanner(types).ForEach(pair =>
        {
            services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
        });
    }
}