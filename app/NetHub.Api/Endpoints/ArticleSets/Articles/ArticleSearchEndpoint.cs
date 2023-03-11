using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Enums;
using NetHub.Extensions;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Articles;

namespace NetHub.Api.Endpoints.ArticleSets.Articles;

[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleSearchEndpoint : Endpoint<ArticleFilter, ArticleModel[]>
{
    [HttpGet("articles/{lang:alpha:length(2)}/search")]
    public override async Task<ArticleModel[]> HandleAsync(ArticleFilter request, CancellationToken ct)
    {
        var userId = UserProvider.TryGetUserId();

        IQueryable<ArticleModel> queryable = Database
            .GetExtendedArticles(userId, loadContributors: request.ContributorUsername is not null)
            .Where(a => a.Status == ContentStatus.Published)
            .Where(a => a.LanguageCode == request.Lang)
            .OrderBy(a => a.Published);

        if (request.ContributorUsername is not null)
            queryable = queryable
                .Where(a => a.Contributors
                    .Any(u => u.UserName == request.ContributorUsername));

        return await queryable.ToArrayAsync(ct);
    }
}