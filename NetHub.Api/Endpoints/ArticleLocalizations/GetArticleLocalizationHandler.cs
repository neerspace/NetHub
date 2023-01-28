using NeerCore.Exceptions;
using NetHub.Application.Services;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Models.Articles.Localizations.One;

internal sealed class GetArticleLocalizationHandler : DbHandler<GetArticleLocalizationRequest, ArticleLocalizationModel>
{
	private readonly IUserProvider _userProvider;

	public GetArticleLocalizationHandler(IServiceProvider serviceProvider, IUserProvider userProvider) : base(serviceProvider)
	{
		_userProvider = userProvider;
	}

	public override async Task<ArticleLocalizationModel> Handle(GetArticleLocalizationRequest request, CancellationToken ct)
	{
		var userId = _userProvider.TryGetUserId();

		var entity = await Database.Set<ArticleLocalization>()
			.Include(l => l.Contributors).ThenInclude(c => c.User)
			.FirstOrDefaultAsync(l =>
				l.ArticleId == request.ArticleId
				&& l.LanguageCode == request.LanguageCode, ct);

		if (entity is null)
			throw new NotFoundException("No such article localization");

		CheckPermissions(entity, userId);

		var localization = entity.Adapt<ArticleLocalizationModel>();

		if (userId is not null)
		{
			var isSaved = await Database.Set<SavedArticle>()
				.SingleOrDefaultAsync(sa => sa.LocalizationId == localization.Id && sa.UserId == userId, ct);
			var articleVote = await Database.Set<ArticleVote>()
				.SingleOrDefaultAsync(sa => sa.ArticleId == localization.ArticleId && sa.UserId == userId, ct);

			localization.IsSaved = isSaved != null;
			localization.SavedDate = isSaved?.SavedDate;
			localization.Vote = articleVote?.Vote;
		}

		localization.Views++;
		await Database.SaveChangesAsync(ct); // TODO: why this task wasn't awaited?

		return localization.Adapt<ArticleLocalizationModel>();
	}

	private void CheckPermissions(ArticleLocalization localization,
		long? userId)
	{
		if (localization.Status == ContentStatus.Published) return;

		if (userId is null || !localization.Contributors.Select(c => c.UserId).Contains(userId.Value))
			throw new PermissionsException();
	}
}