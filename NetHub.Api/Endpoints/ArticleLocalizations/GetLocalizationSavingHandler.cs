using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Models.Articles.Localizations.GetSaving.One;

internal sealed class GetLocalizationSavingHandler : AuthorizedHandler<GetLocalizationSavingRequest, GetLocalizationSavingResult>
{
    public GetLocalizationSavingHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<GetLocalizationSavingResult> Handle(GetLocalizationSavingRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var savedLocalization = await Database.Set<SavedArticle>()
            .Include(sa => sa.Localization)
            .SingleOrDefaultAsync(sa =>
                sa.UserId == userId && sa.Localization!.ArticleId ==
                request.ArticleId && sa.Localization.LanguageCode == request.LanguageCode, ct);

        return new(savedLocalization is not null);
    }
}