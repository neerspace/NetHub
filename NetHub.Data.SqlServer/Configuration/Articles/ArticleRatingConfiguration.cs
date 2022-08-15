using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleRatingConfiguration : IEntityTypeConfiguration<ArticleRating>
{
	public void Configure(EntityTypeBuilder<ArticleRating> builder)
	{
		builder.HasKey(ar => new {LocalizationId = ar.ArticleId, ar.UserId});

		builder.HasOne(ar => ar.User)
			.WithMany()
			.HasForeignKey(ar => ar.UserId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.Property(ar => ar.Rating)
			.HasConversion(r => r.ToString(),
				value => Enum.Parse<Rating>(value));
	}
}