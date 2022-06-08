using MongoDB.Bson.Serialization.Attributes;
using NetHub.Recommendations.Attributes;

namespace NetHub.Recommendations.Entities;

[MongoCollection($"R_{nameof(Tag)}s")]
public record Tag([property: BsonId] long Id);