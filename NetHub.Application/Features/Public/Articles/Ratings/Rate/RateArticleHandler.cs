using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Ratings.Rate;

public class RateArticleHandler : AuthorizedHandler<RateArticleRequest>
{
	public RateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(RateArticleRequest request)
	{
		var userId = UserProvider.GetUserId();

		var rating = await Database.Set<ArticleRating>()
			.Include(ar => ar.Article)
			.Where(ar =>
				ar.ArticleId == request.ArticleId)
			.FirstOrDefaultAsync();

		var article = await Database.Set<Article>()
			.FirstOr404Async(a => a.Id == request.ArticleId);

		switch (request.Rating)
		{
			case RateModel.None when rating is not null:
				Database.Set<ArticleRating>().Remove(rating);
				article.Rate += rating.Rating == Rating.Up ? -1 : 1;
				break;
			case RateModel.Up or RateModel.Down:
			{
				var ratingEntity = new ArticleRating
				{
					ArticleId = article.Id,
					UserId = userId,
					Rating = request.Rating.Adapt<Rating>()
				};

				if (rating is null)
				{
					Database.Set<ArticleRating>().Add(ratingEntity);
					article.Rate += request.Rating == RateModel.Up ? 1 : -1;
				}
				else
				{
					switch (rating.Rating)
					{
						case Rating.Up when request.Rating is RateModel.Down:
							article.Rate -= 2;
							break;
						case Rating.Down when request.Rating is RateModel.Up:
							article.Rate += 2;
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