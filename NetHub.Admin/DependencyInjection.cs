using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Admin.Filters;
using NetHub.Api.Shared.Extensions;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services)
    {
        services.AddNeerApiServices();
        services.AddNeerControllers()
            .AddMvcOptions(options => options.Filters.Add<SuccessStatusCodesFilter>());

        services.ConfigureAllOptions();

        services.AddJwtAuthentication();
        services.AddPoliciesAuthorization();

        services.AddFluentValidationAutoValidation(fv =>
            fv.DisableDataAnnotationsValidation = true);
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<AppUser>(ServiceLifetime.Transient);

        services.AddFluentValidationRulesToSwagger();
    }
}