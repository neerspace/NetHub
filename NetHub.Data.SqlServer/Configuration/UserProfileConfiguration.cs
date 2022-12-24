using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Configuration;

internal class UserProfileConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(e => e.Description).HasMaxLength(256);
		builder.Property(e => e.PhoneNumber).HasMaxLength(32);

		builder.ToTable("UserProfiles");
	}
}