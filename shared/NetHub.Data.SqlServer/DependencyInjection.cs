using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Sieve;
using Sieve.Models;
using Sieve.Services;

namespace NetHub.Data.SqlServer;

public static class DependencyInjection
{
    public static void AddSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseWithUserIdentity();

        services.AddCustomSieve(configuration);
    }

    private static void AddDatabaseWithUserIdentity(this IServiceCollection services)
    {
        var contextFactory = new SqlServerDbContextFactory();
        services.AddDbContext<SqlServerDbContext>(cob => contextFactory.ConfigureContextOptions(cob));

        services.AddIdentityCore<AppUser>(o =>
            {
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SqlServerDbContext>()
            .AddUserManager<UserManager<AppUser>>();

        services.AddScoped<ISqlServerDatabase, SqlServerDbContext>();
    }

    private static void AddCustomSieve(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SieveOptions>(configuration.GetSection(ConfigSectionNames.Sieve).Bind);
        services.AddScoped<ISieveCustomFilterMethods, SieveCustomFiltering>();
        services.AddTransient<ISieveProcessor, SieveProcessor>();
    }
}