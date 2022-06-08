using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
	public void Configure(EntityTypeBuilder<Tag> builder)
	{
		builder.HasKey(t => t.Id);

		builder.HasIndex(t => t.Name);
	}
}