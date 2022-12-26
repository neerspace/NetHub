using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Design;

namespace NetHub.Data.SqlServer.Context;

public class SqlServerDbContextFactory : DbContextFactoryBase<SqlServerDbContext>
{
    public override TextWriter? LogWriter => null;
    public override string SelectedConnectionName => "Default";

    public override string SettingsPath => "../NetHub.Api/appsettings.Development.json";
    // public override string SettingsPath => "./appsettings.Development.json";


    public override SqlServerDbContext CreateDbContext(string[] args) => new(CreateContextOptions());


    public override void ConfigureContextOptions(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString,
            options => options.MigrationsAssembly(MigrationsAssembly));
    }
}