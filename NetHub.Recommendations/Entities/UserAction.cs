using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NetHub.Recommendations.Abstractions.Mongo;

namespace NetHub.Recommendations.Entities;

public class UserAction : IEntity
{
	[BsonId] public ObjectId Id { get; set; }
	public DateTime Date { get; set; }
	public ActionOption ActionOption { get; set; }
	public long UserId { get; set; }
	public long ArticleId { get; set; }
}