namespace NetHub.Api.Shared.Options;

public sealed class CorsOptions
{
    public string[] AllowedOrigins { get; set; } = default!;
}