using FluentValidation;
using MediatR;
using NetHub.Application.Features.Public.Users.CheckUsername;

namespace NetHub.Application.Features.Public.Users.ChangeUsername;

public record ChangeUsernameRequest(string Username) : IRequest;

public class ChangeUsernameValidator : AbstractValidator<CheckUsernameRequest>
{
	public ChangeUsernameValidator()
	{
		RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username not provided");
	}
}