using MongoDB.Bson.Serialization.Attributes;
using NetHub.Recommendations.Abstractions.Mongo;
using NetHub.Recommendations.Attributes;

namespace NetHub.Recommendations.Entities;

[MongoCollection($"R_{nameof(User)}s")]
public class User : IEntity
{
	[BsonId] public long Id { get; set; }
}