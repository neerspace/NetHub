using MongoDB.Driver;

namespace NetHub.Recommendations.Abstractions;

public interface IMongoDbContext
{
	IMongoCollection<TEntity> Set<TEntity>() where TEntity : IEntity;
}