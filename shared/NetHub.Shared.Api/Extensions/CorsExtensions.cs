using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Options;

namespace NetHub.Shared.Api.Extensions;

public static class CorsExtensions
{
    public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = configuration.GetRequiredSection("Cors").Get<CorsOptions>()!;

        services.AddCors(o => o.AddPolicy(Cors.ApiDefault, builder =>
        {
            builder
                .WithOrigins(corsOptions.AllowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }));
    }

    public static void UseCorsPolicy(this WebApplication app) => app.UseCors(Cors.ApiDefault);
}