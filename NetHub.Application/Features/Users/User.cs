namespace NetHub.Application.Features.Users;

public class User
{
	public string Id { get; set; } = default!;
	public string Username { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string? Description { get; set; }
	public DateTime Registered { get; set; }
}