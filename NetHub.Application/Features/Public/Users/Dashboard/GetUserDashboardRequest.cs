using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Users.Dashboard;

public record GetUserDashboardRequest(long UserId) : IRequest<DashboardDto>;

public class GetUserDashboardValidator : AbstractValidator<GetUserDashboardRequest>
{
	public GetUserDashboardValidator()
	{
		RuleFor(r => r.UserId).NotNull().NotEmpty().WithMessage("User Id required");
	}
}