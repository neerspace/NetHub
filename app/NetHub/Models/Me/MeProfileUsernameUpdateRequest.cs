using FluentValidation;

namespace NetHub.Models.Me;

public sealed record MeProfileUsernameUpdateRequest(string Username);

internal sealed class MeProfileUsernameUpdateValidator : AbstractValidator<MeProfileUsernameUpdateRequest>
{
    public MeProfileUsernameUpdateValidator()
    {
        RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username not provided");
    }
}