using MongoDB.Driver;

namespace NetHub.Recommendations.Abstractions.Mongo;

public interface IMongoDbContext
{
	IMongoCollection<TEntity> Set<TEntity>() where TEntity : IEntity;
}