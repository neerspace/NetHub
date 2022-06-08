namespace NetHub.Recommendations.Entities;

public class Suggestion
{
	public long UserId { get; set; }

	public long ArticleId { get; set; }

	public double Rating { get; set; }
}