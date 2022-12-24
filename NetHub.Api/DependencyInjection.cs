using NeerCore.Api.Extensions;
using NetHub.Api.Shared.Extensions;

namespace NetHub.Api;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddNeerApiServices();
        services.AddNeerControllers();

        services.AddJwtAuthentication(configuration);
        services.AddPoliciesAuthorization();

        services.AddCorsPolicies(configuration);
    }
}