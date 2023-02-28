using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer;

public static class DependencyInjection
{
    public static void AddSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseWithUserIdentity();
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
}