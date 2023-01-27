using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Models.Articles;
using NetHub.Application.Models.Users;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public class UserGetArticlesListEndpoint : Endpoint<GetUserArticlesRequest, ArticleModel[]>
{
    [HttpGet("users/{username}/articles")]
    public override async Task<ArticleModel[]> HandleAsync(GetUserArticlesRequest request, CancellationToken ct)
    {
        var username = string.IsNullOrEmpty(request.UserName)
            ? UserProvider.UserName
            : request.UserName.ToUpperInvariant();

        var articles = await Database.Set<Article>()
            .Include(a => a.Localizations)
            .Where(a => a.Author!.NormalizedUserName == username)
            .Skip((request.Page - 1) * request.PerPage)
            .Take(request.PerPage)
            .ProjectToType<ArticleModel>()
            .ToArrayAsync(ct);

        return articles;
    }
}