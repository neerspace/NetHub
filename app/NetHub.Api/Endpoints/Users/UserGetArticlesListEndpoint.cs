using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Articles;
using NetHub.Models.Users;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public class UserGetArticlesListEndpoint : Endpoint<GetUserArticlesRequest, ArticleModelExtended[]>
{
    [HttpGet("users/{username}/articles")]
    public override async Task<ArticleModelExtended[]> HandleAsync(GetUserArticlesRequest request, CancellationToken ct)
    {
        var username = string.IsNullOrEmpty(request.UserName)
            ? UserProvider.UserName
            : request.UserName.ToUpperInvariant();

        var articles = await Database.Set<Article>()
            .Include(a => a.Localizations)
            .Where(a => a.Author!.NormalizedUserName == username)
            .Skip((request.Page - 1) * request.PerPage)
            .Take(request.PerPage)
            .ProjectToType<ArticleModelExtended>()
            .ToArrayAsync(ct);

        return articles;
    }
}