using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetHub.Core.Abstractions.Context;
using NetHub.Data.SqlServer.Configuration.Conversions;
using NetHub.Data.SqlServer.DataSeeding;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Views;
using NetHub.Data.SqlServer.Enums;

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

		builder.Entity<ExtendedUserArticle>(config =>
		{
			config.ToView("v_ExtendedUserArticle")
				.HasKey(ea => new {ea.UserId, ea.LocalizationId});
			config.Property(ea => ea.Status)
				.HasConversion(s => s.ToString(), s => Enum.Parse<ContentStatus>(s));
			config.Property(ea => ea.Vote)
				.HasConversion(s => s.ToString(), s => Enum.Parse<Rating>(s));
		});


		builder.SeedDefaultUser();
		builder.SeedRoles();
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder builder)
	{
		builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConvertor>();
	}
}