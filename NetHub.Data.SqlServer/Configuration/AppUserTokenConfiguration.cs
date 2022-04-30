using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Configuration;

internal class AppUserTokenConfiguration : IEntityTypeConfiguration<AppUserToken>
{
	public void Configure(EntityTypeBuilder<AppUserToken> builder)
	{
		builder.Property(e => e.LoginProvider).HasMaxLength(64);
		builder.Property(e => e.Name).HasMaxLength(128);
		builder.Property(e => e.Value).HasMaxLength(256);

		builder.ToTable("AppUserTokens");
	}
}