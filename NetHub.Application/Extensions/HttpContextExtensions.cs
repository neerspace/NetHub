using System.Net;
using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Extensions;

public static class HttpContextExtensions
{
	/// <summary>
	/// Gets IP address from request headers
	/// </summary>
	public static IPAddress GetIPAddress(this HttpContext httpContext)
	{
		var headers = httpContext.Request.Headers.ToList();

		if (!headers.Exists(h => h.Key == "X-Forwarded-For"))
		{
			// this will always have a value (running locally in development won't have the header)
			return httpContext.Request.HttpContext.Connection.RemoteIpAddress!;
		}

		// when running behind a load balancer you can expect this header
		var header = headers.First(h => h.Key == "X-Forwarded-For").Value.ToString();
		// in case the IP contains a port, remove ':' and everything after
		return IPAddress.Parse(header.Remove(header.IndexOf(':')));
	}

	/// <summary>
	/// Gets user agent from request headers
	/// </summary>
	public static string GetUserAgent(this HttpContext httpContext)
	{
		return httpContext.Request.Headers["User-Agent"].ToString();
	}
}