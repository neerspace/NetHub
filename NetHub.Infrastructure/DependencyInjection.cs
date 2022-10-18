using Microsoft.AspNetCore.Authentication;
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

	public static AuthenticationBuilder AddGoogleAuthProvider(this AuthenticationBuilder builder, IConfiguration configuration)
	{
		var googleOptions = configuration
			.GetSection("Google")
			.Get<GoogleOptions>();

		builder.AddGoogleOpenIdConnect(options =>
		{
			options.ClientId = googleOptions.ClientId;
			options.ClientSecret = googleOptions.ClientSecret;
		});

		return builder;
	}
}