using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Localizations.Create;

public class CreateArticleLocalizationHandler :
    AuthorizedHandler<CreateArticleLocalizationRequest, ArticleLocalizationModel>
{
    private const string UA = nameof(UA);

    public CreateArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<ArticleLocalizationModel> Handle(CreateArticleLocalizationRequest request)
    {
        var userId = UserProvider.GetUserId();
        var article = await Database.Set<Article>()
            .Include(a => a.Localizations)
            .FirstOr404Async(a => a.Id == request.ArticleId);

        if (article.Localizations?.FirstOrDefault(l => l.LanguageCode == UA) is null && request.LanguageCode != UA)
            throw new ApiException("First article must be ukrainian");

        if (article.Localizations?.FirstOrDefault(l => l.LanguageCode == request.LanguageCode) is not null)
            throw new ValidationFailedException("LanguageCode",
                "Article Localization with such language already exists");

        if (await Database.Set<Language>().FirstOrDefaultAsync(l => l.Code == request.LanguageCode) is null)
            throw new ValidationFailedException("LanguageCode", "No such language registered");

        var localization = request.Adapt<ArticleLocalization>();
        localization.Authors = SetAuthors(request.Authors, userId).ToArray();
        localization.Status = ContentStatus.Draft;

        var createdEntity = await Database.Set<ArticleLocalization>().AddAsync(localization);

        await Database.SaveChangesAsync();

        return createdEntity.Entity.Adapt<ArticleLocalizationModel>();
    }

    private static IEnumerable<ArticleAuthor> SetAuthors(ArticleAuthorModel[]? authors, long mainAuthorId)
    {
        var returnAuthors = new List<ArticleAuthor>
        {
            new()
            {
                AuthorId = mainAuthorId,
                Role = ArticleAuthorRole.Author
            }
        };
        if (authors is not null && authors.Length > 0)
            returnAuthors.AddRange(authors.Select(a => a.Adapt<ArticleAuthor>()));

        return returnAuthors;
    }
}