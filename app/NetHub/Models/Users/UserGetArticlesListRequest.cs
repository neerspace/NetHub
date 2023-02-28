using FluentValidation;

namespace NetHub.Models.Users;

public sealed record UserGetArticlesListRequest(
    string? UserName,
    int Page,
    int PerPage
);

internal sealed class GetUserArticlesValidator : AbstractValidator<UserGetArticlesListRequest>
{
    public GetUserArticlesValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page parameter must be greater than or equal to 1");
        RuleFor(x => x.PerPage).GreaterThanOrEqualTo(0).WithMessage("PerPage parameter must be greater than or equal to 0");
    }
}