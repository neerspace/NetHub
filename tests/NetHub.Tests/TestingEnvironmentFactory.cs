using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NetHub.Api;
using Microsoft.EntityFrameworkCore;
using NetHub.Application;
using NetHub.Core.Abstractions.Context;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Tests.Seed;

namespace NetHub.Tests;

public static class TestingEnvironmentFactory
{
	public static IServiceProvider BuildServiceProvider(IConfiguration configuration, IWebHostEnvironment environment)
	{
		var services = new ServiceCollection();
		services.AddSingleton(configuration);
		services.AddSingleton(environment);
		services.AddTestDatabase();

		services.AddScoped(typeof(ILogger<>), typeof(Logger<>));
		services.AddSingleton<ILoggerFactory, LoggerFactory>();

		services.AddWebApi(configuration);
		services.AddApplication(configuration);

		var serviceProvider = services.BuildServiceProvider();

		SeedData(serviceProvider.GetRequiredService<IDatabaseContext>());

		return serviceProvider;
	}

	public static void SeedData(IDatabaseContext context)
	{
		context.Set<User>().AddRange(SeedUsers.Default);
		context.Set<IdentityUserLogin<long>>().AddRange(SeedLogins.Default);
		context.SaveChanges();
	}

	public static IConfiguration BuildConfiguration()
	{
		return new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false)
			.AddEnvironmentVariables()
			.Build();
	}

	public static IWebHostEnvironment BuildEnvironment(string environmentName = "Testing")
	{
		return Mock.Of<IWebHostEnvironment>(env =>
			env.EnvironmentName == environmentName);
	}

	public static void AddTestDatabase(this IServiceCollection services)
	{
		services.AddDbContext<SqlServerDbContext>(cob =>
				cob.UseInMemoryDatabase(databaseName: "Nethub_Tests" + DateTime.Now.ToFileTimeUtc()),
			ServiceLifetime.Transient);
		services.AddTransient<IDatabaseContext, SqlServerDbContext>();
	}
}