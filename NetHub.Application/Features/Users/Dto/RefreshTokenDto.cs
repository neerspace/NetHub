namespace NetHub.Application.Features.Users.Dto;

public class RefreshTokenDto
{
    public string Value { get; set; } = default!;
    public DateTime ExpirationTime { get; set; }
}