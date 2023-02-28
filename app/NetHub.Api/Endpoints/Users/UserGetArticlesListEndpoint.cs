using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Articles;
using NetHub.Models.Users;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Articles;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public class UserGetArticlesListEndpoint : Endpoint<UserGetArticlesListRequest, ArticleModel[]>
{
    [HttpGet("users/{username}/articles"), ClientSide(ActionName = "userArticles")]
    //TODO: Test
    public override async Task<ArticleModel[]> HandleAsync(UserGetArticlesListRequest listRequest, CancellationToken ct)
    {
        var username = string.IsNullOrEmpty(listRequest.UserName)
            ? UserProvider.UserName
            : listRequest.UserName.ToUpperInvariant();

        //New
        var articles = await Database.Set<ArticleContributor>()
            .Include(ac => ac.User)
            .Include(ac => ac.Localization).ThenInclude(l => l.Article)
            .Where(ac => ac.User!.NormalizedUserName == username)
            .Select(ac => ac.Localization!.Article)
            .DistinctBy(a => a.Id)
            .Skip((listRequest.Page - 1) * listRequest.PerPage)
            .Take(listRequest.PerPage)
            .ProjectToType<ArticleModel>()
            .ToArrayAsync(ct);

        //Old
        // var articles = await Database.Set<Article>()
        //     .Include(a => a.Localizations)
        //     .Include(a => a.Author)
        //     .Where(a => a.Author!.NormalizedUserName == username)
        //     .Skip((request.Page - 1) * request.PerPage)
        //     .Take(request.PerPage)
        //     .ProjectToType<ArticleModelExtended>()
        //     .ToArrayAsync(ct);

        return articles;
    }
}