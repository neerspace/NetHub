using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Data.SqlServer.Context;

namespace NetHub.Application.Tools;

public abstract class DbHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    private readonly IServiceProvider _serviceProvider;

    protected readonly ISqlServerDatabase Database;
    protected HttpContext HttpContext => _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext!;

    protected DbHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        Database = serviceProvider.GetRequiredService<ISqlServerDatabase>();
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken ct);
}

public abstract class DbHandler<TRequest> : DbHandler<TRequest, Unit> where TRequest : IRequest
{
    protected DbHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
}