using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Ng.Services;

namespace NetHub.Application.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// Gets IP address from request headers
    /// </summary>
    public static IPAddress GetIPAddress(this HttpContext httpContext)
    {
        var headerValue = httpContext.Request.Headers["X-Forwarded-For"];

        if (headerValue == StringValues.Empty)
        {
            // this will always have a value (running locally in development won't have the header)
            return httpContext.Request.HttpContext.Connection.RemoteIpAddress!;
        }

        // when running behind a load balancer you can expect this header
        var header = headerValue.ToString();

        // in case the IP contains a port, remove ':' and everything after
        int sepIndex = header.IndexOf(':');
        header = sepIndex == -1 ? header : header.Remove(sepIndex);
        return IPAddress.Parse(header);
    }

    /// <summary>
    /// Gets user agent from request headers
    /// </summary>
    public static UserAgent GetUserAgent(this HttpContext httpContext)
    {
        var userAgentService = httpContext.RequestServices.GetRequiredService<IUserAgentService>();
        var userAgentHeader = httpContext.Request.Headers["User-Agent"].ToString();
        return userAgentService.Parse(userAgentHeader);
    }
}