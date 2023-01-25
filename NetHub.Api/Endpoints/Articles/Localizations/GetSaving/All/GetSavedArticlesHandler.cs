using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Views;

namespace NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;

internal sealed class GetSavedArticlesHandler : AuthorizedHandler<GetSavedArticlesRequest, ExtendedArticleModel[]>
{
    public GetSavedArticlesHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<ExtendedArticleModel[]> Handle(GetSavedArticlesRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var saved = await Database.Set<ExtendedUserArticle>()
            .Where(ea => ea.UserId == userId
                         && ea.IsSaved == true
                //TODO: Remove comments in release (please...)
                // && ea.Status == ContentStatus.Published
            )
            .ProjectToType<ExtendedArticleModel>()
            .ToArrayAsync(ct);

        return saved.DistinctBy(s => s.LocalizationId).ToArray();
    }
}