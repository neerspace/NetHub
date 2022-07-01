using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Public.Articles.Localizations.Ratings.Rate;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Articles.Localizations.Ratings.Get;

public class GetLocalizationRateHandler : AuthorizedHandler<GetLocalizationRateRequest, RatingModel>
{
	public GetLocalizationRateHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<RatingModel> Handle(GetLocalizationRateRequest rateRequest)
	{
		var userId = UserProvider.GetUserId();

		var rating = await Database.Set<ArticleRating>()
			.Include(ar => ar.Localization)
			.FirstOrDefaultAsync(ar => ar.Localization != null
			                           && ar.Localization.ArticleId == rateRequest.ArticleId
			                           && ar.Localization.LanguageCode == rateRequest.Code);

		return rating is null ? new RatingModel(RateModel.None) : new RatingModel(rating.Rating.Adapt<RateModel>());
	}
}