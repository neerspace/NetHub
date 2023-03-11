using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.ArticleSets.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleDeleteEndpoint : ActionEndpoint<ArticleQuery>
{
    [HttpDelete("articles/{id:long}/{lang:alpha:length(2)}")]
    public override async Task HandleAsync(ArticleQuery request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var article = await Database.Set<Article>()
            .Include(al => al.Contributors)
            .SingleOrDefaultAsync(al => al.ArticleSetId == request.Id && al.LanguageCode == request.LanguageCode, ct);

        if (article is null)
            throw new NotFoundException("No such article");

        if (article.Contributors.First(a => a.Role == ArticleContributorRole.Author).UserId != userId)
            throw new PermissionsException();

        Database.Set<Article>().Remove(article);

        await Database.SaveChangesAsync(ct);
    }
}