namespace NetHub.Application.Features.Public.Users.Dto;

public class UserProfileDto
{
	public long Id { get; set; }
	public string UserName { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string MiddleName { get; set; } = default!;
	public string Email { get; set; } = default!;
	public bool EmailConfirmed { get; set; }
	public string? Description { get; set; }
	public DateTime Registered { get; set; }
}