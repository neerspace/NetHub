using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using NeerCore.Api.Extensions;
using NetHub.Api.Shared.Extensions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Admin;

public static class DependencyInjection
{
    public static void AddWebAdminApi(this IServiceCollection services)
    {
        services.AddNeerApiServices();

        services.AddJwtAuthentication();
        services.AddPoliciesAuthorization();

        services.AddFluentValidationAutoValidation(fv =>
            fv.DisableDataAnnotationsValidation = true);
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<User>(ServiceLifetime.Transient);

        services.AddFluentValidationRulesToSwagger();
    }
}