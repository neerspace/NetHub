namespace NetHub.Application.Models.Users;

public sealed class UserDto
{
    public long Id { get; set; }
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? ProfilePhotoUrl { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset Registered { get; set; }
}