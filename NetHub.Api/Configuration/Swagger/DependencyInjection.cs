using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace NetHub.Api.Configuration.Swagger;

public static class DependencyInjection
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.ConfigureOptions<SwaggerConfiguration>();
        services.AddSwaggerGen();
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