using MediatR;
using NetHub.Application.Features.Public.Users.Dashboard;

namespace NetHub.Application.Features.Public.Users.Me.Dashboard;

public record GetMyDashboardRequest() : IRequest<DashboardDto>;