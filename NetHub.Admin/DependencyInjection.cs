using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Admin.Filters;
using NetHub.Admin.Infrastructure.Options;
using NetHub.Api.Shared.Extensions;
using NetHub.Application.Interfaces;
using NetHub.Application.Options;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services)
    {
        services.AddNeerApiServices();
        services.AddNeerControllers()
            .AddMvcOptions(options => options.Filters.Add<SuccessStatusCodesFilter>());

        services.ConfigureAllOptions();

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
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<CookieJwtOptions>>().Value;

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

            jwt.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[options.AccessToken.CookieName];
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = async context =>
                {
                    var refreshToken = context.Request.Cookies[options.RefreshToken.CookieName];
                    if (string.IsNullOrEmpty(refreshToken))
                        throw context.Exception;

                    var authService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();
                    await authService.RefreshAsync(refreshToken, context.HttpContext.RequestAborted);
                },
            };
        });
    }
}