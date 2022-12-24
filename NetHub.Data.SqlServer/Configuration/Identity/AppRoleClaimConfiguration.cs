using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppRoleClaimConfiguration : IEntityTypeConfiguration<AppRoleClaim>
{
    public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ClaimType).AsSmallText();
        builder.Property(e => e.ClaimValue).AsLargeText();

        builder.HasOne(e => e.Role).WithMany(e => e.RoleClaims)
            .HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Cascade);
    }
}