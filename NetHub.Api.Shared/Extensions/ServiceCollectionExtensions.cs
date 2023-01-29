using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Api.Shared.Swagger;
using NetHub.Data.SqlServer.Entities.Identity;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Api.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.Configure<SwaggerGenOptions>(options =>
        {
            options.CustomOperationIds(NSwagEndpointNameFactory.Create);
            options.DocumentFilter<ResponsesFilter>();
            options.EnableAnnotations();
            options.SupportNonNullableReferenceTypes();
        });
    }

    public static void AddCustomFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(fv =>
            fv.DisableDataAnnotationsValidation = true);
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<AppUser>(ServiceLifetime.Transient);
        services.AddFluentValidationRulesToSwagger();
    }
}