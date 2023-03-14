using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleSetResourceConfiguration : IEntityTypeConfiguration<ArticleSetResource>
{
    public void Configure(EntityTypeBuilder<ArticleSetResource> builder)
    {
        builder.ToTable($"{nameof(ArticleSetResource)}s").HasKey(ar => ar.ResourceId);

        builder.HasOne(ar => ar.ArticleSet)
            .WithMany(a => a.Images);

        builder.HasOne(ar => ar.Resource)
            .WithMany()
            .HasForeignKey(ar => ar.ResourceId);
    }
}