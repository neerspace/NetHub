using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Users.CheckUsername;

public record CheckUsernameRequest(string Username) : IRequest<CheckUsernameResult>;

public class CheckUsernameValidator : AbstractValidator<CheckUsernameRequest>
{
	public CheckUsernameValidator()
	{
		RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username is required");
	}
}