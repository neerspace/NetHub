using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace NetHub.Api.Configuration.Swagger;

public static class DependencyInjection
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "Nethub Api Swagger",
                Title = "Nethub Api Swagger",
                Version = "1",
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://identity.tacles.net/connect/authorize"),
                        TokenUrl = new Uri("https://identity.tacles.net/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            {"nb.user", "User"},
                            {"nb.admin", "Admin"},
                            {"nb.master", "Master"}
                        }
                    }
                },
                Description = "OAuth 2.0 Authorization"
            });
            
            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });
    }

    public static void UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var swaggerSettings = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSwaggerSettings();
            var apiProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (ApiVersionDescription description in apiProvider.ApiVersionDescriptions)
            {
                string name = $"{swaggerSettings.Title} {description.GroupName.ToUpper()}";
                string url = $"/swagger/{description.GroupName}/swagger.json";
                options.SwaggerEndpoint(url, name);
            }

            options.DocumentTitle = "Swagger - " + swaggerSettings.Title;
            options.InjectStylesheet("/swagger/custom.css");
            options.InjectJavascript("/swagger/custom.js");
            options.OAuthClientId("nethub-api");
            options.OAuthUsePkce();
            options.OAuthClientSecret("199bd7e0c43s694ea6lb816d122fp7x0");
        });
    }

    public static void ForceRedirect(this WebApplication app, string from, string to)
    {
        app.MapGet(from, context =>
        {
            context.Response.Redirect(to, true);
            return Task.CompletedTask;
        });
    }

    public static SwaggerConfigurationOptions GetSwaggerSettings(this IConfiguration configuration)
    {
        var settings = new SwaggerConfigurationOptions();
        configuration.Bind("Swagger", settings);
        return settings;
    }
}