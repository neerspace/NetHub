using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleAuthorConfiguration : IEntityTypeConfiguration<ArticleAuthor>
{
    public void Configure(EntityTypeBuilder<ArticleAuthor> builder)
    {
        builder.HasKey(aa => aa.Id);

        builder.HasOne(aa => aa.Localization)
            .WithMany(al => al.Authors);

        builder.HasOne(aa => aa.Author)
            .WithMany()
            .HasForeignKey(aa => aa.AuthorId);

        builder.Property(aa => aa.Role)
            .HasConversion(ar => ar.ToString(),
                value => Enum.Parse<ArticleAuthorRole>(value));
    }
}