namespace NetHub.Shared.Models.Jwt;

public sealed class RefreshTokenDto
{
    public string Value { get; set; } = default!;
    public DateTimeOffset ExpirationTime { get; set; }
}