using FluentValidation;

namespace NetHub.Models.Users;

public sealed record SearchUsersRequest(string Username);

internal sealed class SearchUserValidator : AbstractValidator<SearchUsersRequest>
{
    public SearchUserValidator()
    {
        RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username required");
    }
}