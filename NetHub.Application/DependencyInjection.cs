using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using HashidsNet;
using Mapster;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using NetHub.Application.Options;
using NetHub.Core.Constants;
using NetHub.Core.DependencyInjection;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHashids(configuration.GetSection("Hashids").Bind);

		services.RegisterServicesFromAssembly("CleanTemplate.Application");

		services.BindConfigurationOptions(configuration);
		services.RegisterMappings();

		services.AddCustomMediatR();
	}


	public static void ConfigureFluentValidation(FluentValidationMvcConfiguration options)
	{
		options.DisableDataAnnotationsValidation = true;
		options.ImplicitlyValidateChildProperties = true;
		options.ImplicitlyValidateRootCollectionElements = true;
	}

	private static void BindConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtOptions>(options =>
		{
			options.Secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));
			options.AccessTokenLifetime = TimeSpan.Parse(configuration["Jwt:AccessTokenLifetime"]);
			options.RefreshTokenLifetime = TimeSpan.Parse(configuration["Jwt:RefreshTokenLifetime"]);
		});

		services.Configure<LocalizationOptions>(configuration.GetSection("Localization"));
	}

	public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddIdentity<AppUser, AppRole>(configuration.GetRequiredSection("Identity").Bind)
				.AddEntityFrameworkStores<SqlServerDbContext>()
				.AddTokenProvider(TokenProviders.Default, typeof(EmailTokenProvider<AppUser>));

		services.Configure<PasswordHasherOptions>(option => option.IterationCount = 7000);
	}

	private static void RegisterMappings(this IServiceCollection services)
	{
		var serviceProvider = services.BuildServiceProvider();
		var hashids = serviceProvider.GetRequiredService<IHashids>();
		var register = new MappingRegister(hashids);

		register.Register(TypeAdapterConfig.GlobalSettings);
	}

	private static void AddCustomMediatR(this IServiceCollection services)
	{
		var assemblies = new[] { Assembly.GetExecutingAssembly() };

		services.AddMediatR(assemblies).AddFluentValidation(ConfigureFluentValidation);
		services.AddFluentValidation(assemblies);
	}
}