using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.DependencyInjection.Extensions;
using NeerCore.Mapping.Extensions;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin;

public static class DependencyInjection
{
    public static void AddAdminApplication(this IServiceCollection services)
    {
        services.AddAllMappers();
        services.AddAllServices(options => options.ResolveInternalImplementations = true);
        services.ConfigureAllOptions();
        services.AddTransient<SignInManager<AppUser>>();
    }
}