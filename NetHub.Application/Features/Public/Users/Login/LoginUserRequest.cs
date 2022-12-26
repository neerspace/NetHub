using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.Login;

public sealed class LoginUserRequest : IRequest<AuthResult>
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}