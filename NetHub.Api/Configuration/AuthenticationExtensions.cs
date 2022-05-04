using IdentityServer4.AccessTokenValidation;
using NetHub.Application.Options;

namespace NetHub.Api.Configuration;

public static class AuthenticationExtensions
{
	public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
	{
		var identityAuthorityUrl = "https://localhost:5001";
		
		services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
			.AddIdentityServerAuthentication(options =>
			{
				// base-address of your identityserver
				options.Authority = identityAuthorityUrl;
				// name of the API resource
				options.ApiName = "NetHub";
			});
	}
}