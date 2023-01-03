using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Admin.Swagger;

public sealed class ResponsesFilter : IDocumentFilter
{
    private const string JsonContent = "application/json";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var operations = swaggerDoc.Paths.Values.SelectMany(v => v.Operations);
        foreach (var operation in operations)
        {
            if (operation.Key is OperationType.Post)
            {
                if (operation.Value.Responses.ContainsKey("201"))
                    continue;

                var response = operation.Value.Responses.First(r => r.Key == "200").Value;
                operation.Value.Responses.Clear();
                response.Description = "Created";
                operation.Value.Responses.Add("201", response);
            }
            else if (operation.Key is OperationType.Put or OperationType.Patch or OperationType.Delete)
            {
                if (operation.Value.Responses.ContainsKey("204")
                    || !operation.Value.Responses.All(r =>
                        r.Value.Content is null || r.Value.Content.Count == 0))
                    continue;

                operation.Value.Responses.Clear();
                operation.Value.Responses.Add("204", new OpenApiResponse { Description = "No Content" });
            }

            operation.Value.Responses.Add("400", new OpenApiResponse
            {
                Description = "Validation Failed",
                Content = ErrorContent,
            });
            operation.Value.Responses.Add("401", new OpenApiResponse
            {
                Description = "Not Authorized",
                Content = ValidationErrorContent,
            });
            operation.Value.Responses.Add("403", new OpenApiResponse
            {
                Description = "Permission Denied",
                Content = ErrorContent,
            });
            operation.Value.Responses.Add("404", new OpenApiResponse
            {
                Description = "Not Found",
                Content = ErrorContent,
            });
            operation.Value.Responses.Add("500", new OpenApiResponse
            {
                Description = "Internal Server Error",
                Content = ErrorContent,
            });
        }

        swaggerDoc.Components.Schemas.Add("ErrorDetails", ErrorDetailsSchema);
        swaggerDoc.Components.Schemas.Add("Error", ErrorSchema(extended: false));
        swaggerDoc.Components.Schemas.Add("ValidationError", ErrorSchema(extended: true));
    }


    private static Dictionary<string, OpenApiMediaType> ErrorContent => new()
    {
        {
            JsonContent, new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Type = "object",
                    Reference = new OpenApiReference
                    {
                        Id = "Error",
                        Type = ReferenceType.Schema
                    }
                }
            }
        }
    };


    private static Dictionary<string, OpenApiMediaType> ValidationErrorContent => new()
    {
        {
            JsonContent, new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Type = "object",
                    Reference = new OpenApiReference
                    {
                        Id = "ValidationError",
                        Type = ReferenceType.Schema
                    }
                }
            }
        }
    };


    private static OpenApiSchema ErrorDetailsSchema => new()
    {
        Type = "object",
        Properties =
        {
            ["field"] = new OpenApiSchema { Type = "string" },
            ["message"] = new OpenApiSchema { Type = "string" },
        }
    };

    private static OpenApiSchema ErrorSchema(bool extended)
    {
        var schema = new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                {
                    "status", new OpenApiSchema
                    {
                        Type = "integer",
                        Format = "int32",
                        Minimum = 400,
                        Maximum = 599,
                        Example = new OpenApiInteger(418)
                    }
                },
                {
                    "type", new OpenApiSchema
                    {
                        Type = "string",
                        Example = new OpenApiString("I'm a teapot")
                    }
                },
                {
                    "message", new OpenApiSchema
                    {
                        Type = "string",
                        Example = new OpenApiString("Server refuses to brew coffee because it is a teapot.")
                    }
                }
            }
        };

        if (extended)
        {
            schema.Properties.Add("errors", new OpenApiSchema
            {
                Type = "array",
                Items = ErrorDetailsSchema,
                Reference = new OpenApiReference
                {
                    Id = "ErrorDetails",
                    Type = ReferenceType.Schema,
                }
            });
        }

        return schema;
    }
}