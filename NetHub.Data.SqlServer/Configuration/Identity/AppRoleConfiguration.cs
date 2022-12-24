using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration.Identity;

internal class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).AsText();
        builder.Property(e => e.NormalizedName).AsText();
        builder.Property(e => e.ConcurrencyStamp).AsText();

        // builder.HasMany(e => e.Claims).WithOne(e => e.Role)
        // .HasForeignKey(e => e.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}