using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleLocalizationConfiguration : IEntityTypeConfiguration<ArticleLocalization>
{
	public void Configure(EntityTypeBuilder<ArticleLocalization> builder)
	{
		builder.HasKey(al => al.Id);

		builder.Property(al => al.Status)
			.HasConversion(s => s.ToString(),
				value => Enum.Parse<ContentStatus>(value));
		builder.Property(al => al.Title).HasMaxLength(128);
		builder.Property(al => al.Description).HasMaxLength(512);
	}
}