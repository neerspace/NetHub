using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Admin.Filters;
using NetHub.Admin.Swagger;
using NetHub.Api.Shared.Extensions;
using NetHub.Data.SqlServer.Entities.Identity;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Admin;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SwaggerGenOptions>(options =>
        {
            options.CustomOperationIds(NSwagEndpointNameFactory.Create);
            options.DocumentFilter<ResponsesFilter>();
            options.EnableAnnotations();
        });

        services.AddNeerApiServices();
        services.AddNeerControllers()
            .AddMvcOptions(options => options.Filters.Add<SuccessStatusCodesFilter>());
        services.AddCorsPolicy(configuration);

        services.ConfigureAllOptions();

        services.AddJwtAuthentication();
        services.AddPoliciesAuthorization();

        services.AddFluentValidation();
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(fv =>
            fv.DisableDataAnnotationsValidation = true);
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<AppUser>(ServiceLifetime.Transient);
        services.AddFluentValidationRulesToSwagger();
    }
}