using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer;

public static class DependencyInjection
{
	public static void AddSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext();
	}


	private static void AddDbContext(this IServiceCollection services)
	{
		var contextFactory = new SqlServerDbContextFactory();
		services.AddDbContext<SqlServerDbContext>(cob => contextFactory.ConfigureContextOptions(cob));
		
		services.AddIdentityCore<User>(o =>
			{
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
			})
			.AddEntityFrameworkStores<SqlServerDbContext>();

		services.AddTransient<UserManager<User>>();
		services.AddTransient<SignInManager<User>>();

		services.AddScoped<IDatabaseContext, SqlServerDbContext>();
	}
}