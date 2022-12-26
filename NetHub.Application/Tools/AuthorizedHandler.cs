using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Interfaces;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Tools;

public abstract class AuthorizedHandler<TRequest, TResult> : DbHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    private readonly IServiceProvider _serviceProvider;

    private IUserProvider? _userProvider;

    protected IUserProvider UserProvider => _userProvider ??= _serviceProvider.GetRequiredService<IUserProvider>();
    protected UserManager<AppUser> UserManager => _serviceProvider.GetRequiredService<UserManager<AppUser>>();
    private long UserId => UserProvider.UserId;


    protected AuthorizedHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}

public abstract class AuthorizedHandler<TRequest> : AuthorizedHandler<TRequest, Unit> where TRequest : IRequest
{
    protected AuthorizedHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
}