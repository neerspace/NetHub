namespace NetHub.Models.Users;

public sealed class PrivateUserResult
{
    public long Id { get; set; }
    public string UserName { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public string? ProfilePhotoUrl { get; set; }
    public DateTimeOffset Registered { get; set; }
}