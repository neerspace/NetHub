using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Shared.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.Configure<SwaggerGenOptions>(options =>
        {
            options.CustomOperationIds(NSwagEndpointNameFactory.Create);
            // Document Filters
            options.DocumentFilter<ResponsesFilter>();
            // Operation Filters
            options.OperationFilter<FormContentTypeSchemaOperationFilter>();
            // Schema Filters
            // options.SchemaFilter<MultiSourceFilter>();
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