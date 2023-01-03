using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppUserLoginConfiguration : IEntityTypeConfiguration<AppUserLogin>
{
    public void Configure(EntityTypeBuilder<AppUserLogin> builder)
    {
        builder.ToTable($"{nameof(AppUserLogin)}s");

        builder.Property(e => e.LoginProvider).AsLargeText();
        builder.Property(e => e.ProviderKey).AsHugeText();
        builder.Property(e => e.ProviderDisplayName).AsText();
    }
}