using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Core.Defaults;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppTokenConfiguration : IEntityTypeConfiguration<AppTokens>
{
    public void Configure(EntityTypeBuilder<AppTokens> builder)
    {
        builder.ToTable($"{nameof(AppTokens)}s").HasKey(e => e.Value);

        builder.Property(e => e.Value).HasMaxLength(128).IsUnicode(false);
        builder.Property(e => e.Device).HasMaxLength(DefaultLimits.Medium).IsUnicode(false);
        builder.Property(e => e.Browser).HasMaxLength(DefaultLimits.Medium).IsUnicode(false);
        builder.Property(e => e.IpAddress).HasMaxLength(45).IsUnicode(false);
        builder.Property(e => e.Created).HasDefaultValueUtcDateTime();

        builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
    }
}