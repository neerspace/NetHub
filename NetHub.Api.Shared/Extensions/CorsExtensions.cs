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
			builder.SetIsOriginAllowed(o => true)
				// .WithOrigins(corsOptions.AllowedOrigins)
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials();
		}));
	}
}