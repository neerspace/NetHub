using System.IdentityModel.Tokens.Jwt;
using NetHub.Api;
using NetHub.Api.Configuration;
using NetHub.Api.Configuration.Swagger;
using NetHub.Api.Middleware;
using NetHub.Application;
using NetHub.Data.SqlServer;
using NetHub.Infrastructure;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using ILogger = NLog.ILogger;

{
	ILogger logger = ConfigureNLog();

	try
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Configuration.AddJsonFile("appsettings.Secrets.json");

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
		LogManager.Shutdown();
	}
}


static void ConfigureBuilder(WebApplicationBuilder builder)
{
	// Change default logging provider to NLog
	builder.Logging.ClearProviders();
	builder.Logging.AddNLogWeb(LogManager.Configuration);

	builder.Services.AddSqlServerDatabase(builder.Configuration);
	builder.Services.AddApplication(builder.Configuration);
	builder.Services.AddInfrastructure();
	builder.Services.AddWebApi(builder.Configuration);

	// Add services to the container.
	builder.Services.AddControllers();

	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
}

static void ConfigureWebApp(WebApplication app)
{
	if (app.Configuration.GetSwaggerSettings().Enabled)
	{
		app.UseCustomSwagger();
		app.ForceRedirect(from: "/", to: "/swagger");
	}

	app.UseCors("Cors");
	app.UseHttpsRedirection();

	app.UseCustomExceptionHandler();

	app.UseAuthentication();
	app.UseAuthorization();
	
	// app.UseMiddleware<ProfilesMiddleware>();
	
	app.MapControllers();
}

static ILogger ConfigureNLog(string loggerConfigName = "NLog.json")
{
	var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile(loggerConfigName, optional: true, reloadOnChange: true).Build();

	LogManager.Configuration = new NLogLoggingConfiguration(config.GetRequiredSection("NLog"));
	return NLogBuilder.ConfigureNLog(LogManager.Configuration).GetLogger("Program");
}