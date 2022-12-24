using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NeerCore.Api.Extensions;
using NetHub.Api.Shared.Extensions;
using NetHub.Application.Options;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services)
    {
        services.AddNeerApiServices();

        services.AddJwtAuthentication();
        services.AddPoliciesAuthorization();

        services.AddFluentValidationAutoValidation(fv =>
            fv.DisableDataAnnotationsValidation = true);
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<AppUser>(ServiceLifetime.Transient);

        services.AddFluentValidationRulesToSwagger();
    }

    private static void AddJwtAuthentication(this IServiceCollection services)
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

                ValidateLifetime = true,
            };
        });
    }
}