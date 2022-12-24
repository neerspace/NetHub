using FluentValidation;
using MediatR;
using NeerCore.Api.Extensions;
using NetHub.Api.Shared.Extensions;
using NetHub.Application.PipelineBehaviors;

namespace NetHub.Api;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddJwtAuthentication();
        services.AddPoliciesAuthorization();

        services.AddNeerApiServices();
        services.AddNeerControllers();

        services.AddCorsPolicies(configuration);
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[] { typeof(Application.DependencyInjection).Assembly });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
    }
}