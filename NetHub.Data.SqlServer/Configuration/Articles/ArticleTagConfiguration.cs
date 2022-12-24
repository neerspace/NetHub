using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
	public void Configure(EntityTypeBuilder<ArticleTag> builder)
	{
		builder.HasKey(at => new {at.TagId, at.ArticleId});
	}
}