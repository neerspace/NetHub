using FluentValidation;
using MediatR;
using NetHub.Application.Extensions;

namespace NetHub.Application.Features.Users.Create;

public class CreateUserCommand : IRequest<User>
{
	/// <example>aspadmin</example>
	public string Username { get; init; } = default!;

	/// <example>aspadmin@asp.net</example>
	public string Email { get; init; } = default!;

	/// <example>aspX1234</example>
	public string Password { get; init; } = default!;
}

internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(o => o.Username).NotEmpty().UserName();
		RuleFor(o => o.Email).NotEmpty().EmailAddress();
		RuleFor(o => o.Password).NotEmpty();
	}
}