using Microsoft.Extensions.DependencyInjection;
using NetHub.Core.DependencyInjection;

namespace NetHub.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services)
	{
		services.RegisterServicesFromAssembly("CleanTemplate.Infrastructure");
	}
}