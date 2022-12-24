using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Public.Users.Dashboard;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Users.Me.Dashboard;

internal sealed class GetMyDashboardHandler : AuthorizedHandler<GetMyDashboardRequest, DashboardDto>
{
    public GetMyDashboardHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<DashboardDto> Handle(GetMyDashboardRequest request, CancellationToken ct)
    {
        var userId = UserProvider.GetUserId();

        var articlesCount = await Database.Set<ArticleContributor>()
            .Where(ac => ac.UserId == userId)
            .CountAsync(ct);
        var articlesViews = await Database.Set<ArticleLocalization>()
            .Include(ar => ar.Contributors)
            .Where(ar => ar.Contributors
                .FirstOrDefault(c => c.UserId == userId) != null && ar.Status == ContentStatus.Published)
            .SumAsync(al => al.Views, ct);

        return new(articlesCount, articlesViews);
    }
}