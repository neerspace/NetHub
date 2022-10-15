using System.Text.Json.Serialization;

namespace NetHub.Application.Models.Facebook;

public class FacebookResponse
{
	[JsonPropertyName("email")] public string Email { get; set; } = default!;
}