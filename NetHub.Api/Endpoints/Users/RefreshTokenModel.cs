namespace NetHub.Application.Features.Public.Users;

// TODO: useless
public class RefreshTokenModel
{
    public string Value { get; set; } = default!;
    public DateTimeOffset ExpirationTime { get; set; }
}