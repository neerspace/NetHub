using MediatR;
using NetHub.Application.Features.Users.Dto;

namespace NetHub.Application.Features.Users.Login;

public class LoginUserRequest : IRequest<AuthResult>
{
	public string Username { get; set; } = default!;
	public string Password { get; set; } = default!;
}