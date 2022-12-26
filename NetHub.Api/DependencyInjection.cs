using NeerCore.Api.Extensions;
using NetHub.Api.Shared.Extensions;
using ExceptionHandlerOptions = NeerCore.Api.ExceptionHandlerOptions;

namespace NetHub.Api;

public static class DependencyInjection
{
	public static void AddWebApi(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddHttpClient();

		services.AddNeerApiServices();

		services.Configure<ExceptionHandlerOptions>(o =>
		{
			o.Extended500ExceptionMessage = true;
			o.HandleFluentValidationExceptions = true;
		});

		services.AddCorsPolicies(configuration);
		
		services.AddNeerControllers();

		services.AddJwtAuthentication(configuration);
		services.AddPoliciesAuthorization();
	}
}