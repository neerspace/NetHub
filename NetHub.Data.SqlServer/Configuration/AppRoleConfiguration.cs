using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Configuration;

internal class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
	public void Configure(EntityTypeBuilder<AppRole> builder)
	{
		builder.Property(e => e.Id).HasColumnType("smallint");
		builder.Property(e => e.Name).HasMaxLength(64);
		builder.Property(e => e.NormalizedName).HasMaxLength(64);
		builder.Property(e => e.ConcurrencyStamp).HasMaxLength(64);

		builder.ToTable("AppRoles");
	}
}