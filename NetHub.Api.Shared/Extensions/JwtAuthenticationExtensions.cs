using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetHub.Application.Options;

namespace NetHub.Api.Shared.Extensions;

public static class JwtAuthenticationExtensions
{
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;

        return services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddCookie().AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = false;

            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = options.Issuer is not null,
                ValidIssuer = options.Issuer,

                ValidateAudience = options.Audiences is not null,
                ValidAudiences = options.Audiences,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = options.Secret,

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
            };
        });

        // var options = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;
        //
        // return services.AddAuthentication(authOptions =>
        // {
        //     authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //     // authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(jwt =>
        // {
        //     jwt.RequireHttpsMetadata = false;
        //     // jwt.SaveToken = true;
        //
        //     jwt.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuer = false,
        //         // ValidateIssuer = options.Issuer is not null,
        //         // ValidIssuer = options.Issuer,
        //
        //         ValidateAudience = false,
        //         // ValidateAudience = options.Audiences is not null,
        //         // ValidAudiences = options.Audiences,
        //
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = options.Secret,
        //
        //         // ValidateLifetime = true,
        //     };
        // });
    }
}