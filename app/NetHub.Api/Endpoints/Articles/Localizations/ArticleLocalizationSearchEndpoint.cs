using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Enums;
using NetHub.Extensions;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Api.Endpoints.Articles.Localizations;

[Tags(TagNames.ArticleLocalizations)]
[ApiVersion(Versions.V1)]
public sealed class ArticleLocalizationSearchEndpoint : Endpoint<ArticleLocalizationFilter, ArticleLocalizationModel[]>
{
    [HttpGet("articles/{lang:alpha:length(2)}/search")]
    public override async Task<ArticleLocalizationModel[]> HandleAsync(ArticleLocalizationFilter request, CancellationToken ct)
    {
        var userId = UserProvider.TryGetUserId();

        IQueryable<ArticleLocalizationModel> queryable = Database
            .GetExtendedArticles(userId, loadContributors: request.ContributorUsername is not null)
            .Where(a => a.Status == ContentStatus.Published)
            .OrderBy(a => a.Published);

        if (request.ContributorUsername is not null)
            queryable = queryable
                .Where(a => a.Contributors
                    .Any(u => u.UserName == request.ContributorUsername));

        return await queryable.ToArrayAsync(ct);
    }
}