using System.Linq.Expressions;
using Sieve.Services;

namespace NetHub.Admin.Infrastructure.Extensions;

public static class SieveExtensions
{
    public static void OpenField<TEntity>(this SievePropertyMapper mapper, Expression<Func<TEntity, object?>> expression, string name) =>
        mapper.Property(expression).CanSort().CanFilter().HasName(name);

    public static void OpenField<TEntity>(this SievePropertyMapper mapper, Expression<Func<TEntity, object?>> expression) =>
        mapper.Property(expression).CanSort().CanFilter();
}