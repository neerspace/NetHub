using Microsoft.EntityFrameworkCore;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Context;

public interface ISqlServerDatabase
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}