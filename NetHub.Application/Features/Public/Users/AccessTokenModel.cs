namespace NetHub.Application.Features.Public.Users;

public class AccessTokenModel
{
    public string Value { get; set; } = default!;
    public DateTime ExpirationTime { get; set; }
}