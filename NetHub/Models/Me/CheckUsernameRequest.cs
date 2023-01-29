using FluentValidation;

namespace NetHub.Models.Me;

public sealed record CheckUsernameRequest(string Username);

internal sealed class CheckUsernameValidator : AbstractValidator<CheckUsernameRequest>
{
    public CheckUsernameValidator()
    {
        RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username is required");
    }
}