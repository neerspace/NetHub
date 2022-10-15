using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Users.Dashboard;

public class GetUserDashboardHandler : DbHandler<GetUserDashboardRequest, DashboardDto>
{
	public GetUserDashboardHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<DashboardDto> Handle(GetUserDashboardRequest request)
	{
		var articlesCount = await Database.Set<ArticleContributor>()
			.Where(ac => ac.UserId == request.UserId)
			.CountAsync();
		var articlesViews = await Database.Set<ArticleLocalization>()
			.Include(ar => ar.Contributors)
			.Where(ar => ar.Contributors
				.SingleOrDefault(c => c.UserId == request.UserId) != null)
			.SumAsync(al => al.Views);
		// .SumAsync(al => )

		return new(articlesCount, articlesViews);
	}
}