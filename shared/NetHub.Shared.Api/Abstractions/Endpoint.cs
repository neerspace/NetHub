using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Data.SqlServer.Context;
using NetHub.Shared.Models;
using NetHub.Shared.Services;

namespace NetHub.Shared.Api.Abstractions;

[Route("v{version:apiVersion}")]
[Produces("application/json")]
public abstract class Endpoint<TRequest, TResponse> : EndpointBaseAsync
    .WithRequest<TRequest>
    .WithResult<TResponse>
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    protected ISqlServerDatabase Database => _database ??= HttpContext.RequestServices.GetRequiredService<ISqlServerDatabase>();
    protected IUserProvider UserProvider => _userProvider ??= HttpContext.RequestServices.GetRequiredService<IUserProvider>();

    public abstract override Task<TResponse> HandleAsync(TRequest request, CancellationToken ct);
}

[Route("v{version:apiVersion}")]
public abstract class ActionEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithoutResult
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    protected ISqlServerDatabase Database => _database ??= HttpContext.RequestServices.GetRequiredService<ISqlServerDatabase>();
    protected IUserProvider UserProvider => _userProvider ??= HttpContext.RequestServices.GetRequiredService<IUserProvider>();

    public abstract override Task HandleAsync(CancellationToken ct);
}

[Route("v{version:apiVersion}")]
public abstract class ActionEndpoint<TRequest> : EndpointBaseAsync
    .WithRequest<TRequest>
    .WithoutResult
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    protected ISqlServerDatabase Database => _database ??= HttpContext.RequestServices.GetRequiredService<ISqlServerDatabase>();
    protected IUserProvider UserProvider => _userProvider ??= HttpContext.RequestServices.GetRequiredService<IUserProvider>();

    public abstract override Task HandleAsync(TRequest request, CancellationToken ct);
}

[Route("v{version:apiVersion}")]
[Produces("application/json")]
public abstract class ResultEndpoint<TResponse> : EndpointBaseAsync
    .WithoutRequest
    .WithResult<TResponse>
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    protected ISqlServerDatabase Database => _database ??= HttpContext.RequestServices.GetRequiredService<ISqlServerDatabase>();
    protected IUserProvider UserProvider => _userProvider ??= HttpContext.RequestServices.GetRequiredService<IUserProvider>();

    public abstract override Task<TResponse> HandleAsync(CancellationToken ct);
}

public abstract class FilterEndpoint<TResponse> : Endpoint<FilterRequest, Filtered<TResponse>> { }

public abstract class FilterEndpoint<TFilter, TResponse> : Endpoint<TFilter, Filtered<TResponse>>
    where TFilter : FilterRequest
{ }