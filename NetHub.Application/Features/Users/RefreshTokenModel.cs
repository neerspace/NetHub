namespace NetHub.Application.Features.Users;

public class RefreshTokenModel
{
    public string Value { get; set; } = default!;
    public DateTime ExpirationTime { get; set; }
}