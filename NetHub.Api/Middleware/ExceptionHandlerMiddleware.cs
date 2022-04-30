#define EXTENDED_500_ERROR_RESPONSE

using System.Net;
using System.Text.Json;
using FluentValidation;
using NetHub.Application.Models;
using NetHub.Core;
using NetHub.Core.Exceptions;
using NLog;
using ILogger = NLog.ILogger;

namespace NetHub.Api.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
	private readonly ILogger _logger;

	public ExceptionHandlerMiddleware()
	{
		_logger = LogManager.GetCurrentClassLogger();
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (ValidationException e)
		{
			await WriteJsonResponseAsync(context, HttpStatusCode.BadRequest, CreateFluentValidationError(e));
		}
		catch (HttpException e)
		{
			if ((int) e.StatusCode >= 500)
				await Write500StatusCodeResponseAsync(context, e);
			else
				await WriteJsonResponseAsync(context, e.StatusCode, CreateError(e));
		}
		catch (Exception e)
		{
			await Write500StatusCodeResponseAsync(context, e);
		}
	}


	private static async Task WriteJsonResponseAsync<T>(HttpContext context, HttpStatusCode statusCode, T response)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int) statusCode;
		await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonConventions.CamelCase));
	}

	private async Task Write500StatusCodeResponseAsync(HttpContext context, Exception exception)
	{
		_logger.Error(exception, "Internal Server Error");

#if EXTENDED_500_ERROR_RESPONSE
		context.Response.ContentType = "text/plain";
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		await context.Response.WriteAsync($"===== SERVER ERROR =====\n{exception}\n===== ===== ===== =====");
#else
		var error = CreateError(new InternalServerException(exception.Message));
		await WriteJsonResponseAsync(context, HttpStatusCode.InternalServerError, error);
#endif
	}

	private static Error CreateError(HttpException e) => new(
		(int) e.StatusCode,
		e.ErrorType,
		e.Message,
		e.Details?.Select(ed => new Error.Details(ed.Field, ed.Message)).ToArray()
	);

	private static Error CreateFluentValidationError(ValidationException e) => new(
		400,
		"ValidationFailed",
		"Invalid model received.",
		e.Errors.Select(ve => new Error.Details(ve.PropertyName, ve.ErrorMessage)).ToArray()
	);
}