using System.Linq.Expressions;
using Sieve.Services;

namespace NetHub.Data.SqlServer.Extensions;

public static class SieveExtensions
{
    public static void AllowFilterAndSort<TEntity>(this SievePropertyMapper mapper, Expression<Func<TEntity, object?>> expression, string name) =>
        mapper.Property(expression).CanSort().CanFilter().HasName(name);

    public static void AllowFilterAndSort<TEntity>(this SievePropertyMapper mapper, Expression<Func<TEntity, object?>> expression) =>
        mapper.Property(expression).CanSort().CanFilter();

    public static void OnlyFilter<TEntity>(this SievePropertyMapper mapper, Expression<Func<TEntity, object?>> expression) =>
        mapper.Property(expression).CanFilter();

    public static void OnlySort<TEntity>(this SievePropertyMapper mapper, Expression<Func<TEntity, object?>> expression) =>
        mapper.Property(expression).CanSort();
}