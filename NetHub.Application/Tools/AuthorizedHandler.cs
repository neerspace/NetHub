using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Services;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Tools;

public class AuthorizedHandler<TRequest, TResult> : DbHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    private readonly IServiceProvider _serviceProvider;

    protected IUserProvider UserProvider;
    protected UserManager<User> UserManager => _serviceProvider.GetRequiredService<UserManager<User>>();

    public AuthorizedHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _serviceProvider = serviceProvider;
        UserProvider = serviceProvider.GetRequiredService<IUserProvider>();
    }
}

public class AuthorizedHandler<TRequest> : AuthorizedHandler<TRequest, Unit> where TRequest : IRequest
{
    public AuthorizedHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}