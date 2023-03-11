using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Extensions;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Api.Endpoints.Me.SavedArticles;

[Authorize]
[Tags(TagNames.MyArticles)]
[ApiVersion(Versions.V1)]
public sealed class SavedArticleListEndpoint : ResultEndpoint<ArticleModel[]>
{
    [HttpGet("me/saved-articles")]
    public override async Task<ArticleModel[]> HandleAsync(CancellationToken ct)
    {
        long userId = UserProvider.UserId;

        var saved = await Database
            .GetExtendedArticles(userId)
            .Where(l => l.IsSaved == true)
            .ToArrayAsync(ct);

        return saved;
    }
}