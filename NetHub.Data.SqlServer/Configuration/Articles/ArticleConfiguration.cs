using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Author)
            .WithMany(au => au.Articles)
            .HasForeignKey(a => a.AuthorId);

        builder.HasMany(a => a.Localizations)
            .WithOne(l => l.Article)
            .HasForeignKey(l => l.ArticleId);

        builder.Property(a => a.Status)
            .HasConversion(s => s.ToString(),
                value => Enum.Parse<ContentStatus>(value));
        builder.Property(a => a.OriginalAuthor).HasMaxLength(32);

        builder.ToTable("Articles");
    }
}