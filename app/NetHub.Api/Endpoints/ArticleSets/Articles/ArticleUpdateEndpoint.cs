using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Core.Constants;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Api.Endpoints.ArticleSets.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleUpdateEndpoint : ActionEndpoint<ArticleUpdateRequest>
{
    [HttpPut("articles/{id:long}/{lang:alpha:length(2)}")]
    public override async Task HandleAsync([FromBody] ArticleUpdateRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        var article = await Database.Set<Article>()
            .Include(al => al.Contributors)
            .SingleOrDefaultAsync(al =>
                al.ArticleSetId == request.Id && al.LanguageCode == request.LanguageCode, ct);
        if (article is null)
            throw new NotFoundException("No such article");

        if (article.Contributors.All(ac => ac.Id != userId))
            throw new PermissionsException();

        if (article.Status == ContentStatus.Published)
            throw new ApiException("You can not edit published article");

        SetNewFields(request, article);

        if (request.Html is not null)
        {
            article.Html = request.Html;
            await HtmlUtility.CheckLinks(Database, request.Id, request.Html);
        }

        if (request.Contributors is not null)
            await SetContributors(article, request.Contributors, ct);

        if (request.NewLanguageCode is not null)
            await SetNewLanguageAsync(request, article, ct);

        article.Updated = DateTimeOffset.UtcNow;
        article.LastContributorId = userId;

        await Database.SaveChangesAsync(ct);
    }

    private async Task SetNewLanguageAsync(ArticleUpdateRequest request, Article localization, CancellationToken ct)
    {
        if (await Database.Set<Article>().CountAsync(l =>
                l.ArticleSetId == request.Id
                && l.LanguageCode == request.NewLanguageCode, ct)
            == 1)
            throw new ValidationFailedException("NewLanguageCode",
                "Article with such language already exists");

        if (request.LanguageCode == ProjectConstants.UA)
            throw new ValidationFailedException("NewLanguageCode", "There is must be one article in ukrainian");

        if (await Database.Set<Language>().FirstOrDefaultAsync(l => l.Code == request.NewLanguageCode, ct) is null)
            throw new ValidationFailedException("LanguageCode", "No such language registered");

        localization.LanguageCode = request.NewLanguageCode!;
    }

    private async Task SetContributors(Article localization, IReadOnlyCollection<ArticleContributorModel> requestContributors, CancellationToken ct)
    {
        if (requestContributors.FirstOrDefault(a => a.Role == ArticleContributorRole.Author) is not null)
            throw new ApiException("You can not set authors");

        var newContributors = localization.Contributors
            .Where(c => c.Role == ArticleContributorRole.Author).ToList();

        foreach (var contributor in requestContributors)
        {
            var count = requestContributors.Count(a => a.UserName == contributor.UserName && a.Role == contributor.Role);
            if (count > 1)
                throw new ApiException("One user can not contribute the same role several times");

            var dbContributor = await Database.Set<AppUser>()
                .FirstOrDefaultAsync(p => p.NormalizedUserName == contributor.UserName.ToUpper(), ct);
            if (dbContributor is null)
                throw new ApiException($"No user with username: {contributor.UserName}");

            newContributors.Add(contributor.Adapt<ArticleContributor>());
        }

        localization.Contributors = newContributors;
        await Database.SaveChangesAsync(ct);
    }

    private static void SetNewFields(ArticleUpdateRequest request, Article article)
    {
        if (request.Title is not null)
            article.Title = request.Title;

        if (request.Description is not null)
            article.Description = request.Description;
    }
}