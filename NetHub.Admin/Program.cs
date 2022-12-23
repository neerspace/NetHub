using NeerCore.Api;
using NeerCore.Api.Extensions;
using NeerCore.Api.Swagger.Extensions;
using NeerCore.Logging;
using NeerCore.Logging.Extensions;
using NetHub.Admin;
using NetHub.Application;
using NetHub.Data.SqlServer;
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

// ==========================================

static void ConfigureBuilder(WebApplicationBuilder builder)
{
    builder.Logging.ConfigureNLogAsDefault();
    builder.Configuration.AddJsonFile("appsettings.Secrets.json");

    builder.Services.AddSqlServerDatabase(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure();
    builder.Services.AddWebAdminApi(builder.Configuration);
}

static void ConfigureWebApp(WebApplication app)
{
    if (app.Configuration.GetSwaggerSettings().Enabled)
        app.UseNeerSwagger();

    app.UseCors(CorsPolicies.AcceptAll);
    app.UseHttpsRedirection();

    app.UseRequestLocalization();
    app.UseNeerExceptionHandler();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

static void MigrateDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<>();
    database.Database.Migrate();
}