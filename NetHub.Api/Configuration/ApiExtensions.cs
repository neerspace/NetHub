using Microsoft.AspNetCore.Mvc;

namespace NetHub.Api.Configuration;

public static class ApiExtensions
{
	public static void AddCorsPolicies(this IServiceCollection services)
	{
		services.AddCors(o => o.AddPolicy("Cors", builder =>
		{
			// TODO: add normal CORS :)
			// 	.WithMethods("POST", "GET", "PUT", "DELETE", "OPTIONS")
			// 	.WithHeaders("Content-Type", "Authorization", "Set-Cookie")
			// 	.AllowCredentials();
			// builder.AllowAnyOrigin()
			builder.WithOrigins("https://testttt-mu.vercel.app")
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials();
		}));
	}

	public static void AddCustomApiVersioning(this IServiceCollection services)
	{
		services.AddApiVersioning();

		services.AddVersionedApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'VVV";
			options.SubstituteApiVersionInUrl = true;
		});
	}

	public static void ConfigureApiBehaviorOptions(this IServiceCollection services)
	{
		services.Configure<ApiBehaviorOptions>(options =>
		{
			// Disable default ModelState validation (because we use only FluentValidation)
			options.SuppressModelStateInvalidFilter = true;
		});
	}
}