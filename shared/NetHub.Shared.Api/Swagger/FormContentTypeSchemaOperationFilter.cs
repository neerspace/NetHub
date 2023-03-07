using System.Reflection;
using Microsoft.OpenApi.Models;
using NetHub.Shared.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Shared.Api.Swagger;

public class FormContentTypeSchemaOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var contentTypeByParameterName = context.MethodInfo.GetParameters()
            .Where(p => p.IsDefined(typeof(ContentTypeEncodingAttribute), true))
            .ToDictionary(p => p.Name!, s => s.GetCustomAttribute<ContentTypeEncodingAttribute>()!.ContentType);

        if (!contentTypeByParameterName.Any())
            return;

        foreach (var requestContent in operation.RequestBody.Content)
        {
            var encodings = requestContent.Value.Encoding;
            foreach (var encoding in encodings)
            {
                if (contentTypeByParameterName.TryGetValue(encoding.Key, out string? value))
                {
                    encoding.Value.ContentType = value;
                }
            }
        }
    }
}