using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Users.Dashboard;

public class GetUserDashboardHandler : DbHandler<GetUserDashboardRequest, DashboardDto>
{
	public GetUserDashboardHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<DashboardDto> Handle(GetUserDashboardRequest request)
	{
		var articlesCount = await Database.Set<ArticleContributor>()
			.Include(ac => ac.Localization)
			.Where(ac => ac.UserId == request.UserId && ac.Localization!.Status == ContentStatus.Published)
			.CountAsync();
		var articlesViews = await Database.Set<ArticleLocalization>()
			.Include(ar => ar.Contributors)
			.Where(ar => ar.Contributors
				.FirstOrDefault(c => c.UserId == request.UserId) != null && ar.Status == ContentStatus.Published)
			.SumAsync(al => al.Views);

		return new(articlesCount, articlesViews);
	}
}