using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Users.Dashboard;

internal sealed class GetUserDashboardHandler : DbHandler<GetUserDashboardRequest, DashboardDto>
{
    public GetUserDashboardHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<DashboardDto> Handle(GetUserDashboardRequest request, CancellationToken ct)
    {
        var articlesCount = await Database.Set<ArticleContributor>()
            .Include(ac => ac.Localization)
            .Where(ac => ac.UserId == request.UserId && ac.Localization!.Status == ContentStatus.Published)
            .CountAsync(ct);
        var articlesViews = await Database.Set<ArticleLocalization>()
            .Include(ar => ar.Contributors)
            .Where(ar => ar.Contributors
                .FirstOrDefault(c => c.UserId == request.UserId) != null && ar.Status == ContentStatus.Published)
            .SumAsync(al => al.Views, ct);

        return new(articlesCount, articlesViews);
    }
}