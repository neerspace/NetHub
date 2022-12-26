using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Infrastructure;

public static class DependencyInjection
{
    public static void AddAdminInfrastructure(this IServiceCollection services)
    {
        services.AddAllServices(options => options.ResolveInternalImplementations = true);
        services.AddTransient<SignInManager<AppUser>>();
    }
}