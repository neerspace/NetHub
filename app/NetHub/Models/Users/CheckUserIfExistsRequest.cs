using FluentValidation;
using NetHub.Shared.Models.Jwt;

namespace NetHub.Models.Users;

public sealed record CheckUserIfExistsRequest(string Key, ProviderType Provider);

internal sealed class CheckUserExistsValidator : AbstractValidator<CheckUserIfExistsRequest>
{
    public CheckUserExistsValidator()
    {
        RuleFor(r => r.Key).NotNull().NotEmpty().WithMessage("Provider key required");
        RuleFor(r => r.Provider).IsInEnum().WithMessage("Wrong provider type");
    }
}