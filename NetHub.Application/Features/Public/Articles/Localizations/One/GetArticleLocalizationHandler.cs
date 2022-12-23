using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Articles.Localizations.One;

public class GetArticleLocalizationHandler : DbHandler<GetArticleLocalizationRequest, ArticleLocalizationModel>
{
	private readonly IUserProvider _userProvider;

	public GetArticleLocalizationHandler(IServiceProvider serviceProvider,
		IUserProvider userProvider) : base(
		serviceProvider)
	{
		_userProvider = userProvider;
	}

	protected override async Task<ArticleLocalizationModel> Handle(GetArticleLocalizationRequest request)
	{
		var userId = _userProvider.TryGetUserId();

		var localization = await Database.Set<ArticleLocalization>()
			.Include(l => l.Contributors)
			.ProjectToType<ArticleLocalizationModel>()
			.FirstOrDefaultAsync(l =>
				l.ArticleId == request.ArticleId
				&& l.LanguageCode == request.LanguageCode);

		if (localization is null)
			throw new NotFoundException("No such article localization");

		if (userId is not null)
		{
			var isSaved = await Database.Set<SavedArticle>()
				.SingleOrDefaultAsync(sa => sa.LocalizationId == localization.Id && sa.UserId == userId);
			var articleVote = await Database.Set<ArticleVote>()
				.SingleOrDefaultAsync(sa => sa.ArticleId == localization.ArticleId);

			localization.IsSaved = isSaved != null;
			localization.SavedDate = isSaved?.SavedDate;
			localization.Vote = articleVote?.Vote;
		}
		
		localization.Views++;
		Database.SaveChangesAsync();
		
		return localization.Adapt<ArticleLocalizationModel>();
	}
}