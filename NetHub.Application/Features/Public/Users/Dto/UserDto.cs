namespace NetHub.Application.Features.Public.Users.Dto;

public class UserDto
{
	public long Id { get; set; }
	public string UserName { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string MiddleName { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string? ProfilePhotoLink { get; set; }
	public bool EmailConfirmed { get; set; }
	public string? Description { get; set; }
	public DateTime Registered { get; set; }
}

public class PrivateUserDto
{
	public long Id { get; set; }
	public string UserName { get; set; } = default!;
	public string? ProfilePhotoLink { get; set; }
}