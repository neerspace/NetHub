using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetHub.Core.Abstractions.Context;
using NetHub.Data.SqlServer.DataSeeding;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Context;

public class SqlServerDbContext : IdentityDbContext<User, AppRole, long,
		IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>, IdentityRoleClaim<long>, RefreshToken>,
	IDatabaseContext

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

		builder.SeedDefaultUser();
		builder.SeedRoles();

		// builder.Entity<UserRole>().HasData(new List<UserProfile>()
		// {

		// })
	}
}