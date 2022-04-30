using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Attributes;
using NetHub.Core.Constants;
using NLog;
using ILogger = NLog.ILogger;

namespace NetHub.Api.Abstractions;

/// <summary>
/// Base API controller
/// </summary>
[ApiController]
[ApiVersion(Versions.V1)]
[Route("/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[AuthorizeRoles(Role.Admin, Role.User)]
public abstract class ApiController : ControllerBase
{
	private IMediator? _mediator;

	protected ILogger Logger => LogManager.GetLogger(GetType().Name);
	protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

	protected void SetNavigationHeaders(int total, int page, int limit)
	{
		Response.Headers["Navigation-Page"] = page.ToString();
		Response.Headers["Navigation-Last-Page"] = ((int) Math.Ceiling((double) total / limit)).ToString();
		Response.Headers["Access-Control-Expose-Headers"] = "Navigation-Page,Navigation-Last-Page";
	}
}