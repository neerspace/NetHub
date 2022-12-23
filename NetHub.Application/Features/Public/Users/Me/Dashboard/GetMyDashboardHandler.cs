using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Public.Users.Dashboard;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Users.Me.Dashboard;

public class GetMyDashboardHandler : AuthorizedHandler<GetMyDashboardRequest, DashboardDto>
{
	public GetMyDashboardHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<DashboardDto> Handle(GetMyDashboardRequest request)
	{
		var userId = UserProvider.GetUserId();

		var articlesCount = await Database.Set<ArticleContributor>()
			.Where(ac => ac.UserId == userId)
			.CountAsync();
		var articlesViews = await Database.Set<ArticleLocalization>()
			.Include(ar => ar.Contributors)
			.Where(ar => ar.Contributors
				.FirstOrDefault(c => c.UserId == userId) != null && ar.Status == ContentStatus.Published)
			.SumAsync(al => al.Views);

		return new(articlesCount, articlesViews);
	}
}