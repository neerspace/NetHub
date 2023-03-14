using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Design;

namespace NetHub.Data.SqlServer.Context;

public class SqlServerDbContextFactory : DbContextFactoryBase<SqlServerDbContext>
{
    public override TextWriter? LogWriter => null;
    public override string SelectedConnectionName => "Default";

    public override string[] SettingsPaths => new[]
    {
        "appsettings.Secrets.json", // for project
        "../../app/NetHub.Api/appsettings.Secrets.json" // relative path for migrations
    };

    public override SqlServerDbContext CreateDbContext(string[] args) => new(CreateContextOptions());


    public override void ConfigureContextOptions(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString,
            options => options.MigrationsAssembly(MigrationsAssembly));
    }
}