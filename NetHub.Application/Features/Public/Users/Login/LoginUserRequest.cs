using MediatR;

namespace NetHub.Application.Features.Public.Users.Login;

public sealed class LoginUserRequest : IRequest<(AuthModel, string)>
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}