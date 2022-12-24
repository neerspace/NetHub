using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Users.Dashboard;

public sealed record GetUserDashboardRequest(long UserId) : IRequest<DashboardDto>;

internal sealed class GetUserDashboardValidator : AbstractValidator<GetUserDashboardRequest>
{
    public GetUserDashboardValidator()
    {
        RuleFor(r => r.UserId).NotNull().NotEmpty().WithMessage("User Id required");
    }
}