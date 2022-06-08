using MediatR;
using NetHub.Application.Features.Users.Dto;

namespace NetHub.Application.Features.Users.Register;

public class RegisterUserRequest : IRequest<UserProfileDto>
{
	public string UserName { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string Password { get; set; } = default!;
	public string PasswordConfirm { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string MiddleName { get; set; } = default!;
}