using FluentValidation;
using NetHub.Application.Models.Jwt;

namespace NetHub.Application.Models.Users;

public sealed record CheckUserExistsRequest(string Login, ProviderType Provider);

internal sealed class CheckUserExistsValidator : AbstractValidator<CheckUserExistsRequest>
{
    public CheckUserExistsValidator()
    {
        RuleFor(r => r.Login).NotNull().NotEmpty().WithMessage("Provider key required");
        RuleFor(r => r.Provider).IsInEnum().WithMessage("Wrong provider type");
    }
}