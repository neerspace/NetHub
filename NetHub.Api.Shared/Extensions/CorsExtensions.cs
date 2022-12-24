using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Api.Shared.Options;

namespace NetHub.Api.Shared.Extensions;

public static class CorsExtensions
{
	public static void AddCorsPolicies(this IServiceCollection services,
		IConfiguration configuration)
	{
		var corsOptions = configuration.GetRequiredSection("Cors").Get<CorsOptions>()!;

		services.AddCors(o => o.AddPolicy(Cors.ApiDefault, builder =>
		{
			// TODO: add normal CORS :)
			// 	.WithMethods("POST", "GET", "PUT", "DELETE", "OPTIONS")
			// 	.WithHeaders("Content-Type", "Authorization", "Set-Cookie")
			// 	.AllowCredentials();
			// builder.AllowAnyOrigin()
			builder.WithOrigins(corsOptions.AllowedOrigins)
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials();
		}));
	}
}