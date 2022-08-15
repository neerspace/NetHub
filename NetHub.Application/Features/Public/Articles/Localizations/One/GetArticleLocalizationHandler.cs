using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Articles.Localizations.One;

public class GetArticleLocalizationHandler : DbHandler<GetArticleLocalizationRequest, ArticleLocalizationModel>
{
	public GetArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<ArticleLocalizationModel> Handle(GetArticleLocalizationRequest request)
	{
		var localization = await Database.Set<ArticleLocalization>()
			.Include(l => l.Contributors)
			.FirstOrDefaultAsync(l =>
				l.ArticleId == request.ArticleId
				&& l.LanguageCode == request.LanguageCode);

		if (localization is null)
			throw new NotFoundException("No such article localization");

		localization.Views++;
		await Database.SaveChangesAsync();

		return localization.Adapt<ArticleLocalizationModel>();
	}
}