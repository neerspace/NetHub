using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetHub.Api.Configuration;
using NetHub.Api.Configuration.Swagger;
using NetHub.Application.Options;
using NetHub.Core.DependencyInjection;

namespace NetHub.Api;

public static class DependencyInjection
{
	public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient();
		services.AddFactoryMiddleware();

		services.BindConfigurationOptions(configuration);

		services.AddCorsPolicies();
		services.AddControllers()
			.AddMvcOptions(ConfigureMvcOptions)
			.AddJsonOptions(ConfigureJsonOptions);

		services.ConfigureApiBehaviorOptions();
		services.AddCustomApiVersioning();
		services.RegisterServicesFromAssembly("NetHub.Api");
		
		var serviceProvider = services.BuildServiceProvider();
		var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>().Value;
		services.AddJwtAuthentication(jwtOptions);
		services.AddPoliciesAuthorization();

		if (configuration.GetSwaggerSettings().Enabled)
			services.AddCustomSwagger();
	}

	private static void BindConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtOptions>(options =>
		{
			options.Secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));
			options.AccessTokenLifetime = TimeSpan.Parse(configuration["Jwt:AccessTokenLifetime"]);
			options.RefreshTokenLifetime = TimeSpan.Parse(configuration["Jwt:RefreshTokenLifetime"]);
		});

		services.Configure<MezhaOptions>(configuration.GetSection("Mezha"));
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

	private static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwt)
	{
		services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
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
			});
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