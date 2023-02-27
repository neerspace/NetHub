using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Constants;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Sieve;
using NetHub.Shared.Options;
using Ng.Services;
using Sieve.Models;
using Sieve.Services;

namespace NetHub;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAllServices(options => options.ResolveInternalImplementations = true);
        services.ConfigureAllOptions();

        services.AddLazyCache();
        services.AddHttpClients(configuration);
        services.AddCustomSieve(configuration);
    }

    private static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var currencyOptions = configuration.GetSection(ConfigSectionNames.CurrencyRate).Get<CurrencyRateOptions>()!;

        services.AddHttpClient(HttpClientNames.CoinGeckoClient, config =>
        {
            config.BaseAddress = new Uri(currencyOptions.CoinGeckoApiUrl);
        });

        services.AddHttpClient(HttpClientNames.MonobankClient, config =>
        {
            config.BaseAddress = new Uri(currencyOptions.MonobankApiUrl);
        });
    }

    private static void AddCustomSieve(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SieveOptions>(configuration.GetSection(ConfigSectionNames.Sieve).Bind);
        services.AddScoped<ISieveCustomFilterMethods, SieveCustomFiltering>();
        services.AddTransient<ISieveProcessor, SieveProcessor>();
    }
}