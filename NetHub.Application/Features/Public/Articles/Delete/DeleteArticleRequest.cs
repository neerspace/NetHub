using MediatR;

namespace NetHub.Application.Features.Public.Articles.Delete;

public sealed record DeleteArticleRequest(long Id) : IRequest;