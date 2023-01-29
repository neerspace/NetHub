namespace NetHub.Models.Users;

public sealed class PrivateUserDto
{
    public long Id { get; set; }
    public string UserName { get; set; } = default!;
    public string? ProfilePhotoUrl { get; set; }
}