using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Localizations.Delete;

public class DeleteArticleLocalizationHandler : AuthorizedHandler<DeleteArticleLocalizationRequest>
{
    public DeleteArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<Unit> Handle(DeleteArticleLocalizationRequest request)
    {
        var userId = UserProvider.GetUserId();

        var localization = await Database.Set<ArticleLocalization>()
            .Include(al => al.Contributors)
            .SingleOrDefaultAsync(al => al.ArticleId == request.ArticleId && al.LanguageCode == request.LanguageCode);

        if (localization is null)
            throw new NotFoundException("No such article localization");

        if (localization.Contributors.First(a => a.Role == ArticleContributorRole.Author).UserId != userId)
            throw new PermissionsException();

        Database.Set<ArticleLocalization>().Remove(localization);

        await Database.SaveChangesAsync();

        return Unit.Value;
    }
}