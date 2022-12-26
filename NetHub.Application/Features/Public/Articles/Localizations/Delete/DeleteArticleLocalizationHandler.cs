using MediatR;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Localizations.Delete;

internal sealed class DeleteArticleLocalizationHandler : AuthorizedHandler<DeleteArticleLocalizationRequest>
{
    public DeleteArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<Unit> Handle(DeleteArticleLocalizationRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var localization = await Database.Set<ArticleLocalization>()
            .Include(al => al.Contributors)
            .SingleOrDefaultAsync(al => al.ArticleId == request.ArticleId && al.LanguageCode == request.LanguageCode, ct);

        if (localization is null)
            throw new NotFoundException("No such article localization");

        if (localization.Contributors.First(a => a.Role == ArticleContributorRole.Author).UserId != userId)
            throw new PermissionsException();

        Database.Set<ArticleLocalization>().Remove(localization);

        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }
}