using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Options;
using NetHub.Core.DependencyInjection;

namespace NetHub.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services)
	{
		services.RegisterServicesFromAssembly("NetHub.Infrastructure");
	}

	public static IServiceCollection AddGoogleAuthProvider(this IServiceCollection services, IConfiguration configuration)
	{
		var googleOptions = configuration
			.GetSection("Google")
			.Get<GoogleOptions>();

		services.AddAuthentication().AddGoogleOpenIdConnect(options =>
		{
			options.ClientId = googleOptions.ClientId;
			options.ClientSecret = googleOptions.ClientSecret;
		});

		return services;
	}
}