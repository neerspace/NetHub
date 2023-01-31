using Microsoft.EntityFrameworkCore;

namespace NetHub.Data.SqlServer.Context;

public interface ISqlServerDatabase
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancel = default);
}