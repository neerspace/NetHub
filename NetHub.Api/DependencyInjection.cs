using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using NetHub.Api.Configuration;
using NetHub.Api.Configuration.Swagger;
using NetHub.Application.Options;

namespace NetHub.Api;

public static class DependencyInjection
{
	public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddFactoryMiddleware();

		services.AddCorsPolicies();
		services.AddControllers()
				.AddMvcOptions(ConfigureMvcOptions)
				.AddJsonOptions(ConfigureJsonOptions);

		services.ConfigureApiBehaviorOptions();
		services.AddCustomApiVersioning();

		var serviceProvider = services.BuildServiceProvider();
		var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>().Value;
		services.AddJwtAuthentication(jwtOptions);
		services.AddPoliciesAuthorization();

		if (configuration.GetSwaggerSettings().Enabled)
		{
			services.AddCustomSwagger();
		}
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