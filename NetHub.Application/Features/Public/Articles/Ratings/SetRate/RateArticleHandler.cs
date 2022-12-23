using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Ratings.SetRate;

public class RateArticleHandler : AuthorizedHandler<RateArticleRequest>
{
	public RateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(RateArticleRequest request)
	{
		var userId = UserProvider.GetUserId();

		var actualVote = await Database.Set<ArticleVote>()
			.Include(av => av.Article)
			.Where(av =>
				av.ArticleId == request.ArticleId && av.UserId == userId)
			.FirstOrDefaultAsync();

		var article = await Database.Set<Article>()
			.FirstOr404Async(a => a.Id == request.ArticleId);

		if (actualVote is null)
		{
			var voteEntity = new ArticleVote
			{
				ArticleId = article.Id,
				UserId = userId,
				Vote = request.Vote 
			};

			article.Rate += request.Vote == Vote.Up ? 1 : -1;
			
			Database.Set<ArticleVote>().Add(voteEntity);
			await Database.SaveChangesAsync();
			
			return Unit.Value;
		}

		switch (request.Vote)
		{
			case Vote.Up:
				//was up
				if (actualVote.Vote == Vote.Up)
				{
					article.Rate -= 1;
					Database.Set<ArticleVote>().Remove(actualVote);
					break;
				}

				//was down
				article.Rate += 2;
				actualVote.Vote = Vote.Up;
				
				break;
			case Vote.Down:
				//was down
				if (actualVote.Vote == Vote.Down)
				{
					article.Rate += 1;
					Database.Set<ArticleVote>().Remove(actualVote);
					break;
				}

				//was up
				article.Rate -= 2;
				actualVote.Vote = Vote.Down;

				break;
		}
		
		await Database.SaveChangesAsync();

		return Unit.Value;
	 }
}