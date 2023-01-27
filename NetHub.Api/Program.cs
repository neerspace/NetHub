using Microsoft.EntityFrameworkCore;
using NeerCore.Api.Extensions;
using NeerCore.Api.Swagger.Extensions;
using NeerCore.Exceptions;
using NeerCore.Logging;
using NeerCore.Logging.Extensions;
using NetHub.Api;
using NetHub.Api.Shared.Extensions;
using NetHub.Application;
using NetHub.Data.SqlServer;
using NetHub.Data.SqlServer.Context;
using NetHub.Infrastructure;

var logger = LoggerInstaller.InitFromCurrentEnvironment();

try
{
    var builder = WebApplication.CreateBuilder(args);
    logger.Debug("Configuring application builder");
    ConfigureBuilder(builder);

    var app = builder.Build();
    logger.Info("Syncing database migrations");
    MigrateDatabase(app);

    logger.Debug("Configuring web application");
    // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    ConfigureWebApp(app);

    app.Run();
}
catch (Exception e)
{
    logger.Fatal(e);
}
finally
{
    logger.Info("Application is now stopping");
}


static void ConfigureBuilder(WebApplicationBuilder builder)
{
    builder.Logging.ConfigureNLogAsDefault();
    builder.Configuration.AddJsonFile("appsettings.Secrets.json");
    builder.Configuration.AddJsonFile("appsettings.Development.json");
    builder.Services.AddSqlServerDatabase();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddWebApi(builder.Configuration, builder.Environment);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

static void ConfigureWebApp(WebApplication app)
{
    app.UseDeveloperExceptionPage();

    if (app.Configuration.GetSwaggerSettings().Enabled)
    {
        app.UseNeerSwagger();
        app.ForceRedirect(from: "/", to: "/swagger");
    }

    app.UseCorsPolicy();

    // app.UseHttpsRedirection();

    app.UseNeerExceptionHandler();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

static void MigrateDatabase(IHost app)
{
    using var scope = app.Services.CreateScope();
    if (scope.ServiceProvider.GetRequiredService<ISqlServerDatabase>() is not SqlServerDbContext database)
        throw new InternalServerException($"{nameof(ISqlServerDatabase)} DB context cannot be resolved");
    database.Database.Migrate();
}