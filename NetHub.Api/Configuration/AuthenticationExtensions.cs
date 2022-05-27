using IdentityServer4.AccessTokenValidation;
using NetHub.Application.Options;

namespace NetHub.Api.Configuration;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        var identityAuthorityUrl = "https://identity.tacles.net/";

        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, 
                options =>
            {
                // options.JwtValidationClockSkew = TimeSpan.FromMinutes(60);
                options.ApiName = "nethub";
                options.Authority = identityAuthorityUrl;
            });
    }
}