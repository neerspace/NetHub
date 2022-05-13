using IdentityServer4.AccessTokenValidation;
using NetHub.Application.Options;

namespace NetHub.Api.Configuration;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        var identityAuthorityUrl = "https://localhost:7501";

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
            .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, 
                options =>
            {
                options.JwtValidationClockSkew = TimeSpan.FromMinutes(60);
                options.ApiName = "nethub";
                options.Authority = identityAuthorityUrl;
                options.RequireHttpsMetadata = false;
            });
        
        services.AddAuthorization();
    }
}