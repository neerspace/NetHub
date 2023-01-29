using FluentValidation;

namespace NetHub.Application.Models.Users;

public sealed record GetUserDashboardRequest(string UserName);

internal sealed class GetUserDashboardValidator : AbstractValidator<GetUserDashboardRequest>
{
    public GetUserDashboardValidator()
    {
        RuleFor(r => r.UserName).NotNull().NotEmpty().WithMessage("Username required");
    }
}