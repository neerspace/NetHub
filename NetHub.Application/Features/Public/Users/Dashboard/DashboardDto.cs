namespace NetHub.Application.Features.Public.Users.Dashboard;

// TODO: don't use DTO sometimes. It should be used or everywhere on nowhere
public sealed record DashboardDto(int ArticlesCount, int ArticlesViews);