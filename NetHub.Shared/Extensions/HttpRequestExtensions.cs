using Microsoft.AspNetCore.Http;

namespace NetHub.Shared.Extensions;

public static class HttpRequestExtensions
{
	public static string GetHostUrl(this HttpRequest request)
		=> $"{request.Scheme}://{request.Host}{request.PathBase}";

	public static string GetResourceUrl(this HttpRequest request, Guid id)
		=> $"{request.GetHostUrl()}/v1/resources/{id}";
}