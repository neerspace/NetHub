using MediatR;
using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Features.Public.Resources.Get;

public record GetResourceRequest(Guid Id) : IRequest<GetResourceResult>;