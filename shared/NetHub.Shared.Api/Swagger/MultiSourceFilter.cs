using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Shared.Api.Swagger;

public class MultiSourceFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        string[] properties = context.Type.GetProperties()
            .Where(p => p.GetCustomAttribute<FromRouteAttribute>() != null)
            .Select(p => p.Name)
            .ToArray();

        foreach (var schemaProperty in schema.Properties)
            if (properties.Contains(schemaProperty.Key, StringComparer.OrdinalIgnoreCase))
                schema.Properties.Remove(schemaProperty);
    }
}