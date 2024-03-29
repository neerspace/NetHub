using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Configuration;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable($"{nameof(Language)}s").HasKey(l => l.Code);

        builder.Property(l => l.Code).HasMaxLength(2);
        builder.Property(l => l.Name).HasMaxLength(512);

        builder.HasOne(l => l.Flag).WithOne()
            .HasForeignKey<Language>(l => l.FlagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}