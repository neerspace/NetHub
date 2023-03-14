using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleSetConfiguration : IEntityTypeConfiguration<ArticleSet>
{
    public void Configure(EntityTypeBuilder<ArticleSet> builder)
    {
        builder.ToTable($"{nameof(ArticleSet)}s").HasKey(a => a.Id);

        builder.HasOne(a => a.Author)
            .WithMany(au => au.ArticleSets)
            .HasForeignKey(a => a.AuthorId);

        builder.HasMany(a => a.Articles)
            .WithOne(l => l.ArticleSet)
            .HasForeignKey(l => l.ArticleSetId);
    }
}