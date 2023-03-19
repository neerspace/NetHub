using FluentValidation;

namespace NetHub.Models.Users;

public sealed class UserSearchRequest
{
    public string[] Usernames { get; init; }
}

internal sealed class SearchUserValidator : AbstractValidator<UserSearchRequest>
{
    public SearchUserValidator()
    {
        RuleFor(r => r.Usernames).NotNull().NotEmpty().WithMessage("At least one Username must be provided");
    }
}