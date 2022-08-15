using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Public.Articles;
using NetHub.Core.Abstractions.Context;

namespace NetHub.Application.Tools;

public class DbHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
	where TRequest : IRequest<TResult>
{
	private readonly IServiceProvider _serviceProvider;

	protected readonly IDatabaseContext Database;
	protected HttpContext HttpContext => _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext!;

	public DbHandler(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		Database = serviceProvider.GetRequiredService<IDatabaseContext>();
	}


	protected virtual Task<TResult> Handle(TRequest request)
	{
		throw new NotImplementedException();
	}

	public virtual Task<TResult> Handle(TRequest request, CancellationToken cancel)
	{
		return Handle(request);
	}
}

public class DbHandler<TRequest> : DbHandler<TRequest, Unit> where TRequest : IRequest
{
	public DbHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}
}