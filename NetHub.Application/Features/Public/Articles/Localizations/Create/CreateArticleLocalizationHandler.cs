using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Application.Tools;
using NetHub.Core.Constants;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Localizations.Create;

internal sealed class CreateArticleLocalizationHandler : AuthorizedHandler<CreateArticleLocalizationRequest, ArticleLocalizationModel>
{
    public CreateArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<ArticleLocalizationModel> Handle(CreateArticleLocalizationRequest request, CancellationToken ct)
    {
        long userId = UserProvider.UserId;
        var article = await Database.Set<Article>()
            .Include(a => a.Localizations)
            .FirstOr404Async(a => a.Id == request.ArticleId, ct);

        if (article.Localizations?.FirstOrDefault(l => l.LanguageCode == ProjectConstants.UA) is null &&
            request.LanguageCode != ProjectConstants.UA)
            throw new ApiException("First article must be ukrainian");

        if (article.Localizations?.FirstOrDefault(l => l.LanguageCode == request.LanguageCode) is not null)
            throw new ValidationFailedException("LanguageCode",
                "Article Localization with such language already exists");

        if (await Database.Set<Language>().FirstOrDefaultAsync(l => l.Code == request.LanguageCode, ct) is null)
            throw new ValidationFailedException("LanguageCode", "No such language registered");

        var localization = request.Adapt<ArticleLocalization>();

        localization.Contributors = (await SetContributors(request.Contributors, userId)).ToArray();
        localization.Status = ContentStatus.Draft;
        localization.InternalStatus = InternalStatus.Created;

        var createdEntity = await Database.Set<ArticleLocalization>().AddAsync(localization, ct);

        // await HtmlTools.CheckLinks(Database, request.ArticleId, request.Html);

        await Database.SaveChangesAsync(ct);

        return createdEntity.Entity.Adapt<ArticleLocalizationModel>();
    }

    private async Task<IEnumerable<ArticleContributor>> SetContributors(ArticleContributorModel[]? contributors,
        long mainAuthorId)
    {
        var returnContributors = new List<ArticleContributor>
        {
            new()
            {
                UserId = mainAuthorId,
                Role = ArticleContributorRole.Author
            }
        };
        if (contributors is not { Length: > 0 }) return returnContributors;

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

            returnContributors.Add(contributor.Adapt<ArticleContributor>());
        }

        return returnContributors;
    }
}