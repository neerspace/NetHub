using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppTokenConfiguration : IEntityTypeConfiguration<AppToken>
{
    public void Configure(EntityTypeBuilder<AppToken> builder)
    {
        builder.ToTable($"{nameof(AppToken)}s").HasKey(e => e.Value);

        builder.Property(e => e.Value).HasMaxLength(128).IsUnicode(false);
        builder.Property(e => e.Created).HasDefaultValueUtcDateTime();
        builder.Property(e => e.Name).AsText().IsRequired();
        builder.Property(e => e.LoginProvider).AsText().IsRequired();

        builder.HasOne(e => e.Device)
            .WithMany()
            .HasForeignKey(e => e.DeviceId);
        builder.HasOne(e => e.User)
            .WithMany(e => e.Tokens)
            .HasForeignKey(e => e.UserId);
    }
}