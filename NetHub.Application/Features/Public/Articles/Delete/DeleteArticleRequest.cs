using MediatR;

namespace NetHub.Application.Features.Public.Articles.Delete;

public record DeleteArticleRequest(long Id) : IRequest;