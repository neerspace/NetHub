using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetHub.Api.Configuration;
using NetHub.Api.Configuration.Swagger;
using NetHub.Api.Middleware;
using NetHub.Application.Options;
using NetHub.Core.DependencyInjection;
using NetHub.Infrastructure;

namespace NetHub.Api;

public static class DependencyInjection
{
	public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient();
		services.AddFactoryMiddleware();

		services.BindConfigurationOptions(configuration);

		services.AddCorsPolicies(configuration);
		services.AddControllers()
			.AddMvcOptions(ConfigureMvcOptions)
			.AddJsonOptions(ConfigureJsonOptions);


		services.ConfigureApiBehaviorOptions();
		services.AddCustomApiVersioning();
		services.RegisterServicesFromAssembly("NetHub.Api");

		var serviceProvider = services.BuildServiceProvider();
		var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>().Value;

		services.AddJwtAuthentication(jwtOptions, configuration);
		services.AddPoliciesAuthorization();

		if (configuration.GetSwaggerSettings().Enabled)
			services.AddCustomSwagger();
	}

	private static void BindConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
	{
		// services.Configure<TelegramOptions>(configuration.GetSection("Google"));
		services.Configure<ProjectOptions>(configuration.GetSection("Configuration"));
	}


	public static void ConfigureMvcOptions(MvcOptions options)
	{
		// To convert ControllerClassNames to kebab-case-style routes
		options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseNamingConvention()));
	}

	private static void ConfigureJsonOptions(JsonOptions options)
	{
		// To serialize enum members as strings in json
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
	}

	private static void AddFluentValidation(this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblies(new[] {typeof(Application.DependencyInjection).Assembly});
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
	}

	private static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwt,
		IConfiguration configuration)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.RequireHttpsMetadata = false;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				// ValidateLifetime = true,
				IssuerSigningKey = jwt.Secret,
				ValidateIssuerSigningKey = true,
			};
		}).AddGoogleAuthProvider(configuration);
	}
}

/// <remarks>
/// kebab-case-example
/// </remarks>
public class KebabCaseNamingConvention : IOutboundParameterTransformer
{
	private static readonly Regex KebabRegex = new("([a-z])([A-Z])");

	public string? TransformOutbound(object? value)
	{
		return value is null ? null : KebabRegex.Replace(value.ToString()!, "$1-$2").ToLower();
	}
}