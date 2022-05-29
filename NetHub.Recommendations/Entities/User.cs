using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NetHub.Recommendations.Attributes;

namespace NetHub.Recommendations.Entities;

[MongoCollection($"R_{nameof(User)}s")]
public class User
{
	[BsonId] public ObjectId Id { get; set; }
}