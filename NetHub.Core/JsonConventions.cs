using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetHub.Core;

public static class JsonConventions
{
	public static readonly JsonSerializerOptions CamelCase = new()
	{
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};

	public static readonly JsonSerializerOptions ExtendedScheme = new()
	{
		ReadCommentHandling = JsonCommentHandling.Skip
	};
}