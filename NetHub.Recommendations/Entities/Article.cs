using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NetHub.Recommendations.Attributes;

namespace NetHub.Recommendations.Entities;

[MongoCollection($"R_{nameof(Article)}s")]
public class Article
{
	[BsonId] public long Id { get; set; }
	public Tag[] Tags { get; set; }
}