using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Core.Defaults;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppTokenConfiguration : IEntityTypeConfiguration<AppToken>
{
    public void Configure(EntityTypeBuilder<AppToken> builder)
    {
        builder.ToTable($"{nameof(AppToken)}s").HasKey(e => e.Value);

        builder.Property(e => e.Value).HasMaxLength(128).IsUnicode(false);
        builder.Property(e => e.Device).HasMaxLength(DefaultLimits.Small).IsUnicode(false);
        builder.Property(e => e.Browser).HasMaxLength(DefaultLimits.Small).IsUnicode(false);
        builder.Property(e => e.BrowserVersion).HasMaxLength(DefaultLimits.Tiny).IsUnicode(false);
        builder.Property(e => e.Ip).HasMaxLength(45).IsUnicode(false);
        builder.Property(e => e.Created).HasDefaultValueUtcDateTime();

        builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
    }
}