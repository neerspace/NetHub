namespace NetHub.Admin.Infrastructure.Models.Users;

public sealed class UserModel
{
    public long Id { get; set; }
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? ProfilePhotoUrl { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool HasPassword { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset Registered { get; set; }
}