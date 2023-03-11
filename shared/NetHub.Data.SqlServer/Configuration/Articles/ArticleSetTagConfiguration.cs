using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Data.SqlServer.Configuration.Articles;

public class ArticleSetTagConfiguration : IEntityTypeConfiguration<ArticleSetTag>
{
    public void Configure(EntityTypeBuilder<ArticleSetTag> builder)
    {
        builder.HasKey(at => new { at.TagId, at.ArticleSetId });
    }
}