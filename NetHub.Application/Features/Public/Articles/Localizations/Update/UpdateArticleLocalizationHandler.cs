using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Localizations.Update;

public class UpdateArticleLocalizationHandler : AuthorizedHandler<UpdateArticleLocalizationRequest>
{
    private const string UA = nameof(UA);

    public UpdateArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<Unit> Handle(UpdateArticleLocalizationRequest request)
    {
        var userId = await UserProvider.GetUserId();
        var localization = await Database.Set<ArticleLocalization>()
            .Include(al => al.Authors)
            .SingleOrDefaultAsync(al =>
                al.ArticleId == request.ArticleId && al.LanguageCode == request.OldLanguageCode);
        if (localization is null)
            throw new NotFoundException("No such article localization");

        if (localization.Authors.First(a => a.Role == ArticleAuthorRole.Author).AuthorId != userId)
            throw new PermissionsException();

        var article = await Database.Set<Article>().Include(a => a.Localizations)
            .FirstOr404Async(a => a.Id == localization.ArticleId);

        if (request.NewLanguageCode is not null && article.Localizations?.Count == 1 && request.NewLanguageCode != UA)
            throw new ValidationFailedException("NewLanguageCode", "There are must be one localization in ukrainian");
        if (request.NewLanguageCode is not null
            && await Database.Set<Language>().FirstOrDefaultAsync(l => l.Code == request.NewLanguageCode) is null)
            throw new ValidationFailedException("LanguageCode", "No such language registered");

        request.Adapt(localization);
        if (request.Authors is not null && request.Authors.Length > 0)
            localization.Authors = SetAuthors(localization.Authors, request.Authors).ToArray();
        if (request.NewLanguageCode is not null) localization.LanguageCode = request.NewLanguageCode;

        await Database.SaveChangesAsync();

        return Unit.Value;
    }

    private static IEnumerable<ArticleAuthor> SetAuthors(IEnumerable<ArticleAuthor> originalAuthors,
        IEnumerable<ArticleAuthorModel> requestAuthors)
    {
        var authors = originalAuthors.Where(oa => oa.Role == ArticleAuthorRole.Author).ToList();
        authors.AddRange(requestAuthors.Select(ra => ra.Adapt<ArticleAuthor>()));

        return authors;
    }
}