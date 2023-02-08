using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Shared.Api.Extensions;
using NetHub.Shared.Api.Filters;

namespace NetHub.Admin.Api;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services)
    {
        services.AddPoliciesAuthorization();
        services.AddJwtAuthentication();
    }
}