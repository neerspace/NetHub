using System.Linq.Expressions;
using NetHub.Application.Models;
using NetHub.Core.Abstractions.Entities;

namespace NetHub.Application.Interfaces;

public interface IFilterService
{
	Task<TModel[]> FilterAsync<TEntity, TModel>(FilterRequest request,
		CancellationToken ct = default, params Expression<Func<TEntity, object>>[]? includes)
		where TEntity : class, IEntity;
}