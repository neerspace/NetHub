using System.Web;

namespace NetHub.Application.Extensions;

public static class UriExtensions
{
	/// <summary>
	/// Adds the specified parameter to the Query String.
	/// </summary>
	/// <param name="uri">Base uri</param>
	/// <param name="parameters">Dictionary of parameters</param>
	/// <returns>Url with added parameter.</returns>
	public static Uri AddQueryParameters(this Uri uri, IReadOnlyDictionary<string, string> parameters)
	{
		var uriBuilder = new UriBuilder(uri);
		var query = HttpUtility.ParseQueryString(uriBuilder.Query);
		foreach (var parameter in parameters)
			query[parameter.Key] = parameter.Value;

		uriBuilder.Query = query.ToString();

		return uriBuilder.Uri;
	}
}