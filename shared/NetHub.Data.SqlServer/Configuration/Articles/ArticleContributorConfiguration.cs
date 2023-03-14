using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleContributorConfiguration : IEntityTypeConfiguration<ArticleContributor>
{
    public void Configure(EntityTypeBuilder<ArticleContributor> builder)
    {
        builder.HasKey(aa => aa.Id);

        builder.HasOne(aa => aa.Article)
            .WithMany(al => al.Contributors)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(aa => aa.User)
            .WithMany()
            .HasForeignKey(aa => aa.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(aa => aa.Role)
            .HasConversion(ar => ar.ToString(),
                value => Enum.Parse<ArticleContributorRole>(value));
    }
}