namespace NetHub.Recommendations.Entities;

public class UserArticleRatings
{
	public long UserId { get; set; }

	public double[] ArticleRatings { get; set; }

	public double Score { get; set; }

	public UserArticleRatings(long userId, int articlesCount)
	{
		UserId = userId;
		ArticleRatings = new double[articlesCount];
	}

	public void AppendRatings(IEnumerable<double> ratings)
	{
		var allRatings = new List<double>();

		allRatings.AddRange(ArticleRatings);
		allRatings.AddRange(ratings);

		ArticleRatings = allRatings.ToArray();
	}
}