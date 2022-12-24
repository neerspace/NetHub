using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Application;
using NetHub.Application.Options;
using NetHub.Infrastructure.Services.Internal.Sieve;
using Sieve.Services;

namespace NetHub.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAllServices(options => options.ResolveInternalImplementations = true);
        services.AddLazyCache();
        services.AddCustomSieve();
        services.AddHttpClients(configuration);
    }

    private static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var currencyOptions = configuration.GetSection("CurrencyRate").Get<CurrencyRateOptions>()!;

        services.AddHttpClient("CoinGeckoClient",
            config => { config.BaseAddress = new Uri(currencyOptions.CoinGeckoApiUrl); });

        services.AddHttpClient("MonobankClient",
            config =>
            {
                config.BaseAddress = new Uri(currencyOptions.MonobankApiUrl);
                // config.Re
            });
    }

    private static void AddCustomSieve(this IServiceCollection services)
    {
        services.AddScoped<ISieveCustomFilterMethods, SieveCustomFiltering>();
        services.AddScoped<ISieveProcessor, NetSieveProcessor>();
    }
}