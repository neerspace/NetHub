using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppDeviceConfiguration : IEntityTypeConfiguration<AppDevice>
{
    public void Configure(EntityTypeBuilder<AppDevice> builder)
    {
        builder.ToTable($"{nameof(AppDevice)}s").HasKey(e => e.Id);

        builder.Property(e => e.Platform).AsSmallText();
        builder.Property(e => e.Browser).AsSmallText();
        builder.Property(e => e.BrowserVersion).AsTinyText().IsUnicode(false);
        builder.Property(e => e.IpAddress).HasMaxLength(45).IsUnicode(false);
        builder.Property(e => e.Status).AsTinyText().IsUnicode(false);
    }
}