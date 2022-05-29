using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NetHub.Recommendations.Attributes;

namespace NetHub.Recommendations.Entities;

[MongoCollection($"R_{nameof(Article)}s")]
public class Article
{
	[BsonId] public ObjectId Id { get; set; }
}