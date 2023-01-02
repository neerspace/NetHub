using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NeerCore.Data.EntityFramework.Design;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Conversions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Entities.Views;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Context;

public class SqlServerDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserClaim,
    AppUserRole, AppUserLogin, AppRoleClaim, AppTokens>, ISqlServerDatabase
{
    public SqlServerDbContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureEntities(config =>
        {
            config.EngineStrategy = DbEngineStrategy.SqlServer;
            config.DateTimeKind = DateTimeKind.Utc;
            config.ApplyDataSeeders = true;
        });

        builder.Entity<ExtendedUserArticle>(config =>
        {
            config.ToView("v_ExtendedUserArticle").HasKey(ea => new { ea.UserId, ea.LocalizationId });
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConvertor>();
        builder.Properties<ContentStatus>().HaveConversion<EnumToStringConverter<ContentStatus>>();
        builder.Properties<Vote>().HaveConversion<EnumToStringConverter<Vote>>();
        builder.Properties<ArticleContributorRole>().HaveConversion<EnumToStringConverter<ArticleContributorRole>>();
    }
}