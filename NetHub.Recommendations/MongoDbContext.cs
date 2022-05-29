using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NetHub.Core.Extensions;
using NetHub.Recommendations.Abstractions;
using NetHub.Recommendations.Attributes;
using NetHub.Recommendations.Options;

namespace NetHub.Recommendations;

public class MongoDbContext : IMongoDbContext
{
	private readonly MongoDbOptions _options;
	private readonly MongoClient _client;
	private IMongoDatabase? _database;

	private IMongoDatabase Database => _database ??= _client.GetDatabase(_options.DatabaseName);


	public MongoDbContext(IOptions<MongoDbOptions> optionsAccessor)
	{
		_options = optionsAccessor.Value;
		_client = new MongoClient(MongoClientSettings.FromConnectionString(_options.ConnectionString));
	}

	public IMongoCollection<TEntity> Set<TEntity>() where TEntity : IEntity
	{
		var collectionName = typeof(TEntity).GetRequiredAttribute<MongoCollectionAttribute>().CollectionName;
		var collection = Database.GetCollection<TEntity>(collectionName);
		return collection;
	}
}