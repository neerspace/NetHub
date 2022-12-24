using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.Abstractions;
using NeerCore.Exceptions;

namespace NetHub.Data.SqlServer.Extensions;

public static class QueryableExtensions
{
    public static async Task<TEntity> FirstOr404Async<TEntity>(this IQueryable<TEntity> queryable,
        CancellationToken cancel = default)
        where TEntity : class, IEntity
    {
        return await queryable.FirstOrDefaultAsync(cancel)
               ?? throw new NotFoundException(typeof(TEntity).Name + " not found.");
    }

    public static async Task<TEntity> FirstOr404Async<TEntity>(this IQueryable<TEntity> queryable,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancel = default)
        where TEntity : class, IEntity
    {
        return await queryable.FirstOrDefaultAsync(predicate, cancel)
               ?? throw new NotFoundException(typeof(TEntity).Name + " not found.");
    }

    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> queryable, string? filter,
        string? sorting, int? skip, int? take)
        where TEntity : class, IEntity
    {
        return queryable
            .AsNoTracking()
            .ApplyFiltering(filter)
            .ApplyPaging(skip, take)
            .ApplySorting(sorting);
    }

    public static IQueryable<TEntity> ApplyFiltering<TEntity>(this IQueryable<TEntity> queryable, string? filter)
        where TEntity : class, IEntity
    {
        if (string.IsNullOrEmpty(filter))
            return queryable;

        return queryable.Where(filter);
    }

    public static IQueryable<TEntity> ApplySorting<TEntity>(this IQueryable<TEntity> queryable, string? sorting)
        where TEntity : class, IEntity
    {
        if (string.IsNullOrEmpty(sorting))
            return queryable;

        return sorting.Split(',').Aggregate(queryable, (current, sortingCase) => current.OrderBy(sortingCase));
    }

    public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> queryable, int? skip, int? take)
        where TEntity : class, IEntity
    {
        if (skip is not null && take is not null)
            return queryable.Skip((int)skip).Take((int)take);

        return queryable;
    }

    public static IQueryable<TEntity> IncludeMany<TEntity>(this IQueryable<TEntity> queryable,
        params string[] inclusions)
        where TEntity : class, IEntity
    {
        if (inclusions.Length <= 0)
            return queryable;

        foreach (string inclusion in inclusions)
            queryable = queryable.Include(inclusion);
        return queryable;
    }
}