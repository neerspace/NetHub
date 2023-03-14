using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(al => al.Id);

        builder.Property(al => al.Status)
            .HasConversion(s => s.ToString(),
                value => Enum.Parse<ContentStatus>(value));
        builder.Property(al => al.Title).HasMaxLength(130);
        builder.Property(al => al.Description).HasMaxLength(500);
        builder.Property(al => al.BanReason).HasMaxLength(500);
    }
}