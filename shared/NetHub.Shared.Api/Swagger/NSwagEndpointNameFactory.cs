using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using NeerCore.Exceptions;

namespace NetHub.Shared.Api.Swagger;

public static class NSwagEndpointNameFactory
{
    public static string Create(ApiDescription x) => $"{GetControllerName(x)}_{GetActionName(x)}";


    private static string GetControllerName(ApiDescription description)
    {
        if (description.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(em => em is ClientSideAttribute) is ClientSideAttribute clientAttr
            && !string.IsNullOrEmpty(clientAttr.Controller))
        {
            return clientAttr.Controller;
        }

        if (description.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(em => em is TagsAttribute) is TagsAttribute tagsAttr)
            return tagsAttr.Tags[0].Replace(" ", "");

        if (description.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            return actionDescriptor.ControllerName.ToCamelCase();

        return description.ActionDescriptor.RouteValues["controller"]?.ToCamelCase()
            ?? throw new InternalServerException($"Invalid action controller: '{description.ActionDescriptor.DisplayName}'");
    }

    private static string GetActionName(ApiDescription description)
    {
        if (description.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(em => em is ClientSideAttribute) is ClientSideAttribute clientAttr
            && !string.IsNullOrEmpty(clientAttr.ActionName))
        {
            return clientAttr.ActionName;
        }

        if (description.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(em => em is HttpMethodAttribute) is HttpMethodAttribute httpAttr)
        {
            string httpMethod = httpAttr.HttpMethods.First().ToLower();

            if (string.IsNullOrEmpty(httpAttr.Template))
                return httpMethod;

            string result = httpAttr.Template.Contains('/')
                ? httpAttr.Template.Split('/')
                    .Skip(1)
                    .FirstOrDefault(s => !s.Contains('{'))
                ?? httpMethod
                : httpMethod switch
                {
                    "post" => "create",
                    "put" => "update",
                    "delete" => "delete",
                    _ => httpMethod
                };

            return result;
        }

        if (description.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            return actionDescriptor.ActionName.ToCamelCase();

        return description.HttpMethod?.ToCamelCase()
            ?? description.ActionDescriptor.RouteValues["action"]?.ToCamelCase()
            ?? description.HttpMethod?.ToCamelCase()
            ?? throw new InternalServerException($"Invalid action: '{description.ActionDescriptor.DisplayName}'");
    }

    private static string ToCamelCase(this string str) =>
        string.IsNullOrEmpty(str) || str.Length < 2
            ? str.ToLowerInvariant()
            : char.ToLowerInvariant(str[0]) + str[1..];
}