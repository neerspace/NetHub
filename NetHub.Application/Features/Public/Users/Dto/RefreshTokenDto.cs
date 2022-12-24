namespace NetHub.Application.Features.Public.Users.Dto;

public sealed class RefreshTokenDto
{
    public string Value { get; set; } = default!;
    public DateTime ExpirationTime { get; set; }
}