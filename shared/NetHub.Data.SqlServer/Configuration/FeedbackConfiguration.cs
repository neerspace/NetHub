using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Data.SqlServer.Configuration;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable($"{nameof(Feedback)}s").HasKey(f => f.Id);

        builder.Property(f => f.Name).AsText();
        builder.Property(f => f.Email).AsText();
    }
}