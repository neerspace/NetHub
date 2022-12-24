using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppUserClaimConfiguration : IEntityTypeConfiguration<AppUserClaim>
{
    public void Configure(EntityTypeBuilder<AppUserClaim> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ClaimType).AsSmallText();
        builder.Property(e => e.ClaimValue).AsLargeText();
    }
}