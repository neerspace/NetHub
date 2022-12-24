using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Application.Features.Public.Articles.Localizations.One;

internal sealed class GetArticleLocalizationHandler : DbHandler<GetArticleLocalizationRequest, ArticleLocalizationModel>
{
    private readonly IUserProvider _userProvider;

    public GetArticleLocalizationHandler(IServiceProvider serviceProvider,
        IUserProvider userProvider) : base(
        serviceProvider)
    {
        _userProvider = userProvider;
    }

    public override async Task<ArticleLocalizationModel> Handle(GetArticleLocalizationRequest request, CancellationToken ct)
    {
        var userId = _userProvider.TryGetUserId();

        var localization = await Database.Set<ArticleLocalization>()
            .Include(l => l.Contributors)
            .ProjectToType<ArticleLocalizationModel>()
            .FirstOrDefaultAsync(l =>
                l.ArticleId == request.ArticleId
                && l.LanguageCode == request.LanguageCode, ct);

        if (localization is null)
            throw new NotFoundException("No such article localization");

        if (userId is not null)
        {
            var isSaved = await Database.Set<SavedArticle>()
                .SingleOrDefaultAsync(sa => sa.LocalizationId == localization.Id && sa.UserId == userId, ct);
            var articleVote = await Database.Set<ArticleVote>()
                .SingleOrDefaultAsync(sa => sa.ArticleId == localization.ArticleId, ct);

            localization.IsSaved = isSaved != null;
            localization.SavedDate = isSaved?.SavedDate;
            localization.Vote = articleVote?.Vote;
        }

        localization.Views++;
        await Database.SaveChangesAsync(ct); // TODO: why this task wasn't awaited?

        return localization.Adapt<ArticleLocalizationModel>();
    }
}