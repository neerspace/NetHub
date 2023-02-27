using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Core.Enums;
using NetHub.Data.SqlServer.Conversions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Context;

public class SqlServerDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserClaim,
    AppUserRole, AppUserLogin, AppRoleClaim, AppToken>, ISqlServerDatabase
{
    public SqlServerDbContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyEntityDating(options => options.DateTimeKind = DateTimeKind.Utc);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        builder.AddLocalizedStrings(GetType().Assembly);
        builder.ApplyAllDataSeeders();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.ApplyLocalizedStringConversions();
        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConvertor>();
        builder.Properties<ArticleContributorRole>().HaveConversion<EnumToStringConverter<ArticleContributorRole>>();
        builder.Properties<ContentStatus>().HaveConversion<EnumToStringConverter<ContentStatus>>();
        builder.Properties<DeviceStatus>().HaveConversion<EnumToStringConverter<DeviceStatus>>();
        builder.Properties<Vote>().HaveConversion<EnumToStringConverter<Vote>>();
    }
}