using System.Linq.Expressions;
using NeerCore.Data.Abstractions;
using NetHub.Shared.Models;

namespace NetHub.Shared.Services;

public interface IFilterService
{
    Task<TModel[]> FilterAsync<TEntity, TModel>(
        FilterRequest request, CancellationToken ct = default, params Expression<Func<TEntity, object>>[]? includes)
        where TEntity : class, IEntity;

    Task<Filtered<TModel>> FilterWithCountAsync<TEntity, TModel>(FilterRequest request, CancellationToken ct = default)
        where TEntity : class, IEntity;
}