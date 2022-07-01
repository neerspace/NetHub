using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Localizations.Ratings.Rate;

public class RateLocalizationHandler : AuthorizedHandler<RateLocalizationRequest>
{
	public RateLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(RateLocalizationRequest request)
	{
		var userId = UserProvider.GetUserId();

		var rating = await Database.Set<ArticleRating>()
			.Include(r => r.Localization)
			.Where(r => r.Localization != null &&
			            r.Localization.ArticleId == request.ArticleId &&
			            r.Localization.LanguageCode == request.LanguageCode)
			.FirstOrDefaultAsync();

		var localization = await Database.Set<ArticleLocalization>()
			.FirstOr404Async(al => al.ArticleId == request.ArticleId && al.LanguageCode == request.LanguageCode);

		switch (request.Rating)
		{
			case RateModel.None when rating is not null:
				Database.Set<ArticleRating>().Remove(rating);
				localization.Rate += rating.Rating == Rating.Up ? -1 : 1;
				break;
			case RateModel.Up or RateModel.Down:
			{
				var ratingEntity = new ArticleRating
				{
					LocalizationId = localization.Id,
					UserId = userId,
					Rating = request.Rating.Adapt<Rating>()
				};

				if (rating is null)
				{
					Database.Set<ArticleRating>().Add(ratingEntity);
					localization.Rate += request.Rating == RateModel.Up ? 1 : -1;
				}
				else
				{
					switch (rating.Rating)
					{
						case Rating.Up when request.Rating is RateModel.Down:
							localization.Rate -= 2;
							break;
						case Rating.Down when request.Rating is RateModel.Up:
							localization.Rate += 2;
							break;
					}

					rating.Rating = request.Rating.Adapt<Rating>();
				}

				break;
			}
		}

		await Database.SaveChangesAsync();

		return Unit.Value;
	}
}