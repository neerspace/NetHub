using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetHub.Api.Configuration.Swagger;

public class SwaggerConfiguration : IConfigureNamedOptions<SwaggerGenOptions>
{
	private readonly IWebHostEnvironment _environment;
	private readonly SwaggerConfigurationOptions _options;
	private readonly IApiVersionDescriptionProvider _provider;

	public SwaggerConfiguration(IConfiguration configuration, IWebHostEnvironment environment, IApiVersionDescriptionProvider provider)
	{
		_options = configuration.GetSwaggerSettings();
		_environment = environment;
		_provider = provider;
	}

	public void Configure(SwaggerGenOptions options)
	{
		// add swagger document for every API version discovered
		foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
			options.SwaggerDoc(description.GroupName.ToLower(), CreateVersionInfo(description));

		// options.CustomSchemaIds(NameSchemaIdSelector);

		foreach (string xmlPath in GetXmlComments())
			options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
	}

	public void Configure(string name, SwaggerGenOptions options) => Configure(options);


	private OpenApiInfo CreateVersionInfo(ApiVersionDescription versionDescription)
	{
		return new OpenApiInfo
		{
			Title = _options.Title,
			Version = versionDescription.ApiVersion.ToString(),
			Description = File.ReadAllText(Path.Join(_environment.ContentRootPath, _options.Description)),
		};
	}

	private IEnumerable<string> GetXmlComments()
	{
		// Set the comments path for the Swagger JSON and UI
		if (_options.IncludeComments is { Length: > 0 })
		{
			foreach (string xmlDocsFile in _options.IncludeComments)
				yield return Path.Combine(AppContext.BaseDirectory, xmlDocsFile);
		}
	}

	private static string NameSchemaIdSelector(Type type) =>
			type.FullName!
					.Replace("CleanTemplate.Core.", "")
					.Replace("CleanTemplate.Application.Features.", "")
					.Replace("CleanTemplate.Application.", "");
}