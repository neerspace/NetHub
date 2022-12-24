using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NeerCore.Data.EntityFramework.Design;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Conversions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Entities.Views;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Context;

public class SqlServerDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserClaim,
    AppUserRole, AppUserLogin, AppRoleClaim, RefreshToken>, ISqlServerDatabase
{
    public SqlServerDbContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.AddAllEntities();
        builder.ConfigureEntities(config =>
        {
            config.EngineStrategy = DbEngineStrategy.SqlServer;
            config.DateTimeKind = DateTimeKind.Utc;
            config.ApplyDataSeeders = true;
            config.DataAssemblies = new[] { GetType().Assembly, };
        });

        builder.Entity<ExtendedUserArticle>(config =>
        {
            config.ToView("v_ExtendedUserArticle")
                .HasKey(ea => new { ea.UserId, ea.LocalizationId });
            config.Property(ea => ea.Status)
                .HasConversion(s => s.ToString(), s => Enum.Parse<ContentStatus>(s));
            config.Property(ea => ea.Vote)
                .HasConversion(s => s.ToString(), s => s == null ? null : Enum.Parse<Vote>(s));
            config.Property(ea => ea.ContributorRole)
                .HasConversion(s => s.ToString(), s => Enum.Parse<ArticleContributorRole>(s));
            config.Property(ea => ea.Status)
                .HasConversion(s => s.ToString(), s => Enum.Parse<ContentStatus>(s));
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConvertor>();
        builder.Properties<ContentStatus>().HaveConversion<EnumToStringConverter<ContentStatus>>();
    }
}