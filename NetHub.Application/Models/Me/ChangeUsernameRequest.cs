using FluentValidation;

namespace NetHub.Application.Models.Me;

public sealed record ChangeUsernameRequest(string Username);

internal sealed class ChangeUsernameValidator : AbstractValidator<CheckUsernameRequest>
{
    public ChangeUsernameValidator()
    {
        RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username not provided");
    }
}