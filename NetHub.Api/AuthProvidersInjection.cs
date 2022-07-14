using NetHub.Infrastructure;

namespace NetHub.Api;

public static class AuthProvidersInjection
{
	public static void AddAuthProviders(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddGoogleAuthProvider(configuration);
	}
}