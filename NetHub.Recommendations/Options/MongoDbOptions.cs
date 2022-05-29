namespace NetHub.Recommendations.Options;

public class MongoDbOptions
{
	public string ConnectionString { get; set; } = default!;
	public string DatabaseName { get; set; } = default!;
}