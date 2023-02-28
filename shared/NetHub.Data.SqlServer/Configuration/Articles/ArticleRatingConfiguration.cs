using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleRatingConfiguration : IEntityTypeConfiguration<ArticleVote>
{
    public void Configure(EntityTypeBuilder<ArticleVote> builder)
    {
        builder.HasKey(ar => new { LocalizationId = ar.ArticleId, ar.UserId });

        builder.HasOne(ar => ar.User)
            .WithMany()
            .HasForeignKey(ar => ar.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(ar => ar.Vote)
            .HasConversion(r => r.ToString(),
                value => Enum.Parse<Vote>(value));
    }
}