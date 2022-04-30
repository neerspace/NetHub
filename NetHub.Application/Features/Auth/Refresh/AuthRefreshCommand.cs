using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Auth.Refresh;

public class AuthRefreshCommand : IRequest<AuthResult>
{
	/// <example>[Base64]</example>
	public string RefreshToken { get; init; } = default!;
}

public class AuthRefreshCommandValidator : AbstractValidator<AuthRefreshCommand>
{
	public AuthRefreshCommandValidator()
	{
		RuleFor(o => o.RefreshToken).NotEmpty();
	}
}