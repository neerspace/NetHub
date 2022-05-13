using MediatR;

namespace NetHub.Application.Features.Public.Articles.One;

public record GetArticleRequest(long Id) : IRequest<ArticleModel>;