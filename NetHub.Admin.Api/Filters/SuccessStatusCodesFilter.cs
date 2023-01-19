using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetHub.Admin.Api.Filters;

public class SuccessStatusCodesFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.StatusCode = context.HttpContext.Request.Method switch
        {
            "POST" => StatusCodes.Status201Created,
            "PUT" or "PATCH" or "DELETE" when context.Result is not ObjectResult => StatusCodes.Status204NoContent,
            _ => context.HttpContext.Response.StatusCode
        };
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}