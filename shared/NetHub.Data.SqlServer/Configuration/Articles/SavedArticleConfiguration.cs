using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class SavedArticleConfiguration : IEntityTypeConfiguration<SavedArticle>
{
    public void Configure(EntityTypeBuilder<SavedArticle> builder)
    {
        builder.HasKey(sa => new { sa.UserId, sa.LocalizationId });

        builder.HasOne(sa => sa.User)
            .WithMany(u => u.SavedArticles)
            .HasForeignKey(sa => sa.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(sa => sa.Localization)
            .WithMany()
            .HasForeignKey(sa => sa.LocalizationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}