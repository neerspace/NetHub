using MediatR;

namespace NetHub.Application.Features.Public.Resources.Get;

public sealed record GetResourceRequest(Guid Id) : IRequest<GetResourceResult>;