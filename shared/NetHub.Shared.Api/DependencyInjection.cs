using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeerCore.Api;
using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using NetHub.Shared.Api.Extensions;
using NetHub.Shared.Api.Filters;

namespace NetHub.Shared.Api;

public static class DependencyInjection
{
    public static void AddSharedApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.Configure<ExceptionHandlerOptions>(o =>
            o.Extended500ExceptionMessage = environment.IsDevelopment());

        services.AddNeerApiServices();
        services.AddNeerControllers()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<SuccessStatusCodesFilter>();
            });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressInferBindingSourcesForParameters = true;
        });

        services.AddCorsPolicy(configuration);

        services.ConfigureAllOptions();
        services.AddCustomSwagger();
        services.AddCustomFluentValidation();
    }
}