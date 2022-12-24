using System.IdentityModel.Tokens.Jwt;
using NeerCore.Api.Extensions;
using NeerCore.Api.Swagger.Extensions;
using NeerCore.Logging;
using NeerCore.Logging.Extensions;
using NetHub.Api;
using NetHub.Api.Shared;
using NetHub.Application;
using NetHub.Data.SqlServer;
using NetHub.Infrastructure;

var logger = LoggerInstaller.InitFromCurrentEnvironment();

try
{
    var builder = WebApplication.CreateBuilder(args);
    // builder.Configuration.AddJsonFile("appsettings.Secrets.json");

    ConfigureBuilder(builder);

    var app = builder.Build();
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
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
    builder.Services.AddSqlServerDatabase(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddWebApi(builder.Configuration);

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

    app.UseCors(Cors.ApiDefault);

    app.UseHttpsRedirection();

    app.UseNeerExceptionHandler();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}