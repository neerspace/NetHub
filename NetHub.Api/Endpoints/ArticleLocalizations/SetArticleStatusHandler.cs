using MediatR;
using NetHub.Application.Extensions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Models.Articles.Localizations.Status.Publish;

internal sealed class SetArticleStatusHandler : AuthorizedHandler<SetArticleStatusRequest>
{
    public SetArticleStatusHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<Unit> Handle(SetArticleStatusRequest statusRequest, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var localization = await Database.Set<ArticleLocalization>()
            .Include(al => al.Contributors)
            .FirstOr404Async(al => al.ArticleId == statusRequest.Id && al.LanguageCode == statusRequest.LanguageCode, ct);

        CheckStatuses(statusRequest, localization, userId);

        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }

    private static void CheckStatuses(SetArticleStatusRequest request, ArticleLocalization localization, long userId)
    {
        if (request.Status == ArticleStatusRequest.Publish)
        {
            if (localization.Status == ContentStatus.Published)
                return;

            if (localization.GetAuthorId() != userId ||
                localization.Status is not
                    (ContentStatus.Draft or ContentStatus.Pending))
                throw new PermissionsException();
            localization.Status = ContentStatus.Pending;
        }

        if (request.Status == ArticleStatusRequest.UnPublish)
        {
            if (localization.GetAuthorId() != userId ||
                localization.Status is not
                    (ContentStatus.Draft or ContentStatus.Published or ContentStatus.Pending))
                throw new PermissionsException();
            localization.Status = ContentStatus.Draft;
        }
    }
}