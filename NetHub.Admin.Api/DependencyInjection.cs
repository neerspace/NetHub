using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Api.Shared.Extensions;
using NetHub.Api.Shared.Filters;

namespace NetHub.Admin.Api;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsPolicy(configuration);

        services.AddNeerApiServices();
        services.AddNeerControllers()
            .AddMvcOptions(options => options.Filters.Add<SuccessStatusCodesFilter>());

        services.ConfigureAllOptions();
        services.AddCustomSwagger();
        services.AddCustomFluentValidation();

        services.AddPoliciesAuthorization();
        services.AddJwtAuthentication();
    }
}