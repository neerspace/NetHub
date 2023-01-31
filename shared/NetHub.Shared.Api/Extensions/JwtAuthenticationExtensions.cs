using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetHub.Shared.Options;

namespace NetHub.Shared.Api.Extensions;

public static class JwtAuthenticationExtensions
{
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value.AccessToken;

        return services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;

                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    // Issuer
                    ValidateIssuer = options.Issuer is not null,
                    ValidIssuer = options.Issuer,
                    // Audience
                    ValidateAudience = options.Audiences is not null,
                    ValidAudiences = options.Audiences,
                    // Secret
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = options.Secret,
                    // Lifetime
                    ValidateLifetime = true,
                    // Allowed lifetime extra
                    ClockSkew = options.ClockSkew == TimeSpan.Zero
                        ? TimeSpan.FromMinutes(5)
                        : options.ClockSkew,
                };
            });
    }
}