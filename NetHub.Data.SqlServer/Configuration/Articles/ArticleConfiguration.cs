using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

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
    }
}