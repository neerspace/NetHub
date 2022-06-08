using MongoDB.Driver;
using NetHub.Recommendations.Abstractions.Mongo;
using NetHub.Recommendations.Entities;

namespace NetHub.Recommendations.Services;

public class UserBehaviorTransformer
{
	private readonly IMongoDbContext _database;

	public UserBehaviorTransformer(IMongoDbContext database)
	{
		_database = database;
	}

	public async Task<UserArticleRatingsTable> GetUserArticleRatingsTable(IRater rater)
	{
		var table = new UserArticleRatingsTable();

		var users = await _database.Set<User>().Find({});
		// .AsQueryable().ToListAsync();
		var articles = await _database.Set<User>().AsQueryable().ToListAsync();
		var actions = await _database.Set<UserAction>().AsQueryable().ToListAsync();

		table.UserIndexToId = users.OrderBy(x => x.Id).Select(x => x.Id).Distinct().ToList();
		table.ArticleIndexToId = articles.OrderBy(x => x.Id).Select(x => x.Id).Distinct().ToList();

		foreach (var userId in table.UserIndexToId)
		{
			table.Users.Add(new UserArticleRatings(userId, table.ArticleIndexToId.Count));
		}

		var userArticleRatingGroup = actions
			.GroupBy(x => new {x.UserId, x.ArticleId})
			.Select(g => new {g.Key.UserId, g.Key.ArticleId, Rating = rater.GetRating(g.ToList())})
			.ToList();

		foreach (var userAction in userArticleRatingGroup)
		{
			int userIndex = table.UserIndexToID.IndexOf(userAction.UserID);
			int articleIndex = table.ArticleIndexToID.IndexOf(userAction.ArticleID);

			table.Users[userIndex].ArticleRatings[articleIndex] = userAction.Rating;
		}

		return table;
	}
}