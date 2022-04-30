using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Configuration;

internal class AppRoleClaimConfiguration : IEntityTypeConfiguration<AppRoleClaim>
{
	public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
	{
		builder.Property(e => e.RoleId).HasColumnType("smallint");
		builder.Property(e => e.ClaimType).HasMaxLength(32);
		builder.Property(e => e.ClaimValue).HasMaxLength(128);

		builder.ToTable("AppRoleClaims");
	}
}