namespace NetHub.Application.Features.Public.Users.Dto;

public sealed class RefreshTokenDto
{
    public string Value { get; set; } = default!;
    public DateTimeOffset ExpirationTime { get; set; }
}