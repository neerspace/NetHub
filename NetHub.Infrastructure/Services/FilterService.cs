using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Interfaces;
using NetHub.Application.Models;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.Abstractions.Entities;
using NetHub.Core.DependencyInjection;
using Sieve.Models;
using Sieve.Services;

namespace NetHub.Infrastructure.Services;

[Inject]
public class FilterService : IFilterService
{
	private readonly IDatabaseContext _database;
	private readonly ISieveProcessor _sieveProcessor;

	public FilterService(IDatabaseContext database, ISieveProcessor sieveProcessor)
	{
		_database = database;
		_sieveProcessor = sieveProcessor;
	}

	public async Task<TModel[]> FilterAsync<TEntity, TModel>(FilterRequest request,
		CancellationToken ct = default, params Expression<Func<TEntity, object>>[]? includes)
		where TEntity : class, IEntity  
	{
		var dbSet = _database.Set<TEntity>()
			.AsQueryable()
			.AsNoTracking();
		var sieveModel = request.Adapt<SieveModel>();
		var queryable = _sieveProcessor.Apply(sieveModel,
			includes is not null
				? includes.Aggregate(dbSet,
					(current, include) => current.Include(include))
				: dbSet);
		var entities = await queryable.ToArrayAsync(ct);

		return entities.Select(e => e.Adapt<TModel>()).ToArray();
	}
} 