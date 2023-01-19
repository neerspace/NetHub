using NeerCore.Logging;
using NeerCore.Logging.Extensions;

var logger = LoggerInstaller.InitFromCurrentEnvironment();

try
{
    var builder = WebApplication.CreateBuilder(args);
    logger.Debug("Configuring application builder");
    ConfigureBuilder(builder);
    var app = builder.Build();
    // logger.Info("Syncing database migrations");
    // MigrateDatabase(app);
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

// Add services to the container
static void ConfigureBuilder(WebApplicationBuilder builder)
{
    builder.Logging.ConfigureNLogAsDefault();

    builder.Services.AddControllersWithViews();
}

// Configure the HTTP request pipeline
static void ConfigureWebApp(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        // The default HSTS value is 30 days.
        // You may want to change this for production scenarios,
        // see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapFallbackToFile("index.html");
}