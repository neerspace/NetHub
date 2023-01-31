namespace NetHub.Shared.Api.Options;

public sealed class CorsOptions
{
    public string[] AllowedOrigins { get; set; } = default!;
}