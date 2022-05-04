using Microsoft.EntityFrameworkCore;
using NetHub.Core.Abstractions.Context;

namespace NetHub.Data.SqlServer.Context;

public class SqlServerDbContext : DbContext, IDatabaseContext
{
    public SqlServerDbContext(DbContextOptions options) : base(options)
    {
        // To use AsNoTracking by default
        // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var entitiesAssembly = GetType().Assembly;
        builder.ApplyConfigurationsFromAssembly(entitiesAssembly);
    }
}