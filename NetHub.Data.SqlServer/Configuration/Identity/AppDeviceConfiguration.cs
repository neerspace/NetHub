using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Core.Defaults;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppDeviceConfiguration : IEntityTypeConfiguration<AppDevice>
{
    public void Configure(EntityTypeBuilder<AppDevice> builder)
    {
        builder.ToTable($"{nameof(AppDevice)}s").HasKey(e => e.Id);

        builder.Property(e => e.Platform).HasMaxLength(DefaultLimits.Small).IsUnicode(false);
        builder.Property(e => e.Browser).HasMaxLength(DefaultLimits.Small).IsUnicode(false);
        builder.Property(e => e.BrowserVersion).HasMaxLength(DefaultLimits.Tiny).IsUnicode(false);
        builder.Property(e => e.IpAddress).HasMaxLength(45).IsUnicode(false);
    }
}