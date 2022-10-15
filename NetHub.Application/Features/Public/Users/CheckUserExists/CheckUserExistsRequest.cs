using FluentValidation;
using MediatR;
using NetHub.Application.Features.Public.Users.Sso;

namespace NetHub.Application.Features.Public.Users.CheckUserExists;

public record CheckUserExistsRequest(string Key, ProviderType Provider) : IRequest<CheckUserExistsResult>;

public class CheckUserExistsValidator : AbstractValidator<CheckUserExistsRequest>
{
	public CheckUserExistsValidator()
	{
		RuleFor(r => r.Key).NotNull().NotEmpty().WithMessage("Provider key required");
		RuleFor(r => r.Provider).IsInEnum().WithMessage("Wrong provider type");
	}
}