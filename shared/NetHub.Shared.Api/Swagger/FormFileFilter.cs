using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Shared.Api.Swagger;

public class FormFileFilter : ISchemaFilter
{
    private const string NullableContextAttributeName = "System.Runtime.CompilerServices.NullableContextAttribute";

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null
            && context.Type != typeof(IFormFile))
        {
            return;
        }

        var isNullable = context.Type.GetCustomAttributesData()
            .Any(a => a.AttributeType.FullName.Equals(NullableContextAttributeName));

        if (isNullable)
        {
            schema.Nullable = true;
        }
    }

    // public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    // {
    //     foreach (var description in context.ApiDescriptions)
    //     {
    //         if (description is null)
    //         {
    //             continue;
    //         }
    //
    //         var fileDescriptions = description.ParameterDescriptions
    //             .Where(
    //                 d => d.ModelMetadata is DefaultModelMetadata dmm &&
    //                      dmm.Attributes.Attributes.Any(at => at is SwaggerNullableAttribute));
    //
    //         foreach (var fileDescription in fileDescriptions)
    //             fileDescription.IsRequired = false;
    //     }
    // }


    // public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    // {
    //     if (schema?.Properties == null || context?.Type == null)
    //         return;
    //
    //     var nullableGenericProperties = context.Type.GetProperties()
    //         .Where(t =>
    //             t.GetCustomAttribute<SwaggerNullableAttribute>() != null);
    //
    //     var customAttributes = context.Type.GetProperties();
    //
    //     foreach (var excludedProperty in nullableGenericProperties)
    //     {
    //         if (schema.Properties.ContainsKey(excludedProperty.Name.ToLowerInvariant()))
    //         {
    //             var prop = schema.Properties[excludedProperty.Name.ToLowerInvariant()];
    //
    //             prop.Nullable = true;
    //             prop.Required = new HashSet<string>() {"false"};
    //         }
    //     }
    // }
}