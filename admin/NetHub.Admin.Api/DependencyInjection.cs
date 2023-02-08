using NetHub.Shared.Api.Extensions;

namespace NetHub.Admin.Api;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services)
    {
        services.AddPoliciesAuthorization();
        services.AddJwtAuthentication();
    }
}