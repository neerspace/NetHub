using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Configuration;

internal class AppUserLoginConfiguration : IEntityTypeConfiguration<AppUserLogin>
{
	public void Configure(EntityTypeBuilder<AppUserLogin> builder)
	{
		builder.Property(e => e.LoginProvider).HasMaxLength(128);
		builder.Property(e => e.ProviderKey).HasMaxLength(512);
		builder.Property(e => e.ProviderDisplayName).HasMaxLength(128);

		builder.ToTable("AppUserLogins");
	}
}