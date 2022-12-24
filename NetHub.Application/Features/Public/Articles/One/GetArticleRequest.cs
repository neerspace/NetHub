using MediatR;

namespace NetHub.Application.Features.Public.Articles.One;

public sealed record GetArticleRequest(long Id) : IRequest<(ArticleModel, Guid[]?)>;