using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
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
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Articles;

namespace NetHub.Api.Endpoints.ArticleSets.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleCreateEndpoint : Endpoint<ArticleCreateRequest, ArticleModel>
{
    [HttpPost("articles/{id:long}/{lang}"), ClientSide(ActionName = "create")]
    public override async Task<ArticleModel> HandleAsync([FromBody] ArticleCreateRequest request, CancellationToken ct)
    {
        long userId = UserProvider.UserId;

        var articleSet = await Database.Set<ArticleSet>()
            .Include(a => a.Articles)
            .FirstOr404Async(a => a.Id == request.Id, ct);

        if (articleSet.Articles?.FirstOrDefault(l => l.LanguageCode == ProjectConstants.UK) is null && request.LanguageCode != ProjectConstants.UK)
            throw new ApiException("First article must be ukrainian");

        if (articleSet.Articles?.FirstOrDefault(l => l.LanguageCode == request.LanguageCode) is not null)
            throw new ValidationFailedException("LanguageCode",
                "Article with such language already exists");

        if (await Database.Set<Language>().FirstOrDefaultAsync(l => l.Code == request.LanguageCode, ct) is null)
            throw new ValidationFailedException("LanguageCode", "No such language registered");

        var articleEntity = request.Adapt<Article>();

        articleEntity.Contributors = (await SetContributors(request.Contributors, userId)).ToArray();
        articleEntity.Status = ContentStatus.Draft;

        var createdEntity = await Database.Set<Article>().AddAsync(articleEntity, ct);

        // await HtmlTools.CheckLinks(Database, request.ArticleId, request.Html);

        await Database.SaveChangesAsync(ct);

        return createdEntity.Entity.Adapt<ArticleModel>();
    }

    private async Task<IEnumerable<ArticleContributor>> SetContributors(ArticleContributorModel[]? contributors, long mainAuthorId)
    {
        var returnContributors = new List<ArticleContributor>
        {
            new()
            {
                UserId = mainAuthorId,
                Role = ArticleContributorRole.Author
            }
        };
        if (contributors is not { Length: > 0 })
            return returnContributors;

        if (contributors.FirstOrDefault(a => a.Role == ArticleContributorRole.Author) is not null)
            throw new ApiException("You can not set authors");

        foreach (var contributor in contributors)
        {
            var count = contributors.Count(a => a.UserName == contributor.UserName && a.Role == contributor.Role);
            if (count > 1)
                throw new ApiException("One user can not contribute the same role several times");

            var dbContributor = await Database.Set<AppUser>()
                .SingleOrDefaultAsync(p => p.NormalizedUserName == contributor.UserName.ToUpper());
            if (dbContributor is null)
                throw new NotFoundException($"No user with username: {contributor.UserName}");

            var returnContributor = contributor.Adapt<ArticleContributor>();
            returnContributor.UserId = dbContributor.Id;

            returnContributors.Add(returnContributor);
        }

        return returnContributors;
    }
}