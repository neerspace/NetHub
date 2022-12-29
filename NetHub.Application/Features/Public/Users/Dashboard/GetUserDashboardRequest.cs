using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Users.Dashboard;

public sealed record GetUserDashboardRequest(string UserName) : IRequest<DashboardDto>;

internal sealed class GetUserDashboardValidator : AbstractValidator<GetUserDashboardRequest>
{
    public GetUserDashboardValidator()
    {
        RuleFor(r => r.UserName).NotNull().NotEmpty().WithMessage("Username required");
    }
}