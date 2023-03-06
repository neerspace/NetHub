using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NetHub.Data.SqlServer.Context;

public interface ISqlServerDatabase
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancel = default);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}