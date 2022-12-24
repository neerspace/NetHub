using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHub.Core.Defaults;

namespace NetHub.Data.SqlServer.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasDefaultValueUtcDateTime<TProperty>(this PropertyBuilder<TProperty> builder)
    {
        return builder.HasDefaultValueSql("GETUTCDATE()");
    }

    /// <summary>Limit: 10</summary>
    public static PropertyBuilder<TProperty> AsTinyText<TProperty>(this PropertyBuilder<TProperty> builder) =>
        builder.HasMaxLength(DefaultLimits.Tiny);

    /// <summary>Limit: 30</summary>
    public static PropertyBuilder<TProperty> AsSmallText<TProperty>(this PropertyBuilder<TProperty> builder) =>
        builder.HasMaxLength(DefaultLimits.Small);

    /// <summary>Limit: 50</summary>
    public static PropertyBuilder<TProperty> AsText<TProperty>(this PropertyBuilder<TProperty> builder) =>
        builder.HasMaxLength(DefaultLimits.Medium);

    /// <summary>Limit: 100</summary>
    public static PropertyBuilder<TProperty> AsLargeText<TProperty>(this PropertyBuilder<TProperty> builder) =>
        builder.HasMaxLength(DefaultLimits.Large);

    /// <summary>Limit: 300</summary>
    public static PropertyBuilder<TProperty> AsHugeText<TProperty>(this PropertyBuilder<TProperty> builder) =>
        builder.HasMaxLength(DefaultLimits.Huge);
}