using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Options;

namespace NetHub.Shared;

public static class DependencyInjection
{
    public static void AddSharedApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterMappings();
        services.ConfigureOptions(configuration);
        services.AddTransient<SignInManager<AppUser>>();
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
        // var register = new MappingRegister();

        // register.Register(TypeAdapterConfig.GlobalSettings);
    }
}