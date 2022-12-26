namespace NetHub.Admin.Infrastructure.Models.Users;

public sealed class User
{
    public long Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime Registered { get; set; }
}