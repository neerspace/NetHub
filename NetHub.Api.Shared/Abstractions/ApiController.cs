using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetHub.Api.Shared.Abstractions;

/// <summary>
/// Base API controller
/// </summary>
[ApiController]
[ApiVersion(Versions.V1)]
[Route("/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public abstract class ApiController : ControllerBase
{
    private IMediator? _mediator;

    protected ILogger Logger => HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(GetType().Name);
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected void SetNavigationHeaders(int total, int page, int limit)
    {
        Response.Headers["Navigation-Page"] = page.ToString();
        Response.Headers["Navigation-Last-Page"] = ((int)Math.Ceiling((double)total / limit)).ToString();
        Response.Headers["Access-Control-Expose-Headers"] = "Navigation-Page,Navigation-Last-Page";
    }
}