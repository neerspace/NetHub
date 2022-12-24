using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetHub.Application.Options;

namespace NetHub.Admin.Extensions;

public static class JwtAuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;

        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = false;
            jwt.SaveToken = true;

            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = options.Issuer is not null,
                ValidIssuer = options.Issuer,

                ValidateAudience = options.Audiences is not null,
                ValidAudiences = options.Audiences,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = options.Secret,

                ValidateLifetime = true
            };

            // TODO: Uncomment to use with SignalR
            // jwt.Events = new JwtBearerEvents
            // {
            //     OnMessageReceived = context =>
            //     {
            //         if (!context.HttpContext.Request.Path.StartsWithSegments("/hubs"))
            //             return Task.CompletedTask;
            //
            //         var accessToken = context.Request.Query["token"];
            //
            //         if (!string.IsNullOrEmpty(accessToken))
            //             context.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
            //
            //         return Task.CompletedTask;
            //     }
            // };
        });
    }
}