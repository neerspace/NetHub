using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using NetHub.Application.Models;

namespace NetHub.Admin.Abstractions;

[Route("v{version:apiVersion}")]
[Produces("application/json")]
public abstract class Endpoint<TRequest, TResponse> : EndpointBaseAsync
    .WithRequest<TRequest>
    .WithResult<TResponse> { }

[Route("v{version:apiVersion}")]
public abstract class ActionEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithoutResult { }

[Route("v{version:apiVersion}")]
public abstract class ActionEndpoint<TRequest> : EndpointBaseAsync
    .WithRequest<TRequest>
    .WithoutResult { }

[Route("v{version:apiVersion}")]
[Produces("application/json")]
public abstract class ResultEndpoint<TResponse> : EndpointBaseAsync
    .WithoutRequest
    .WithResult<TResponse> { }

public abstract class FilterEndpoint<TResponse> : Endpoint<FilterRequest, Filtered<TResponse>> { }

public abstract class FilterEndpoint<TFilter, TResponse> : Endpoint<TFilter, Filtered<TResponse>>
    where TFilter : FilterRequest { }