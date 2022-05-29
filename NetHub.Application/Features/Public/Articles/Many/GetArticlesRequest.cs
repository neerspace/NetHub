using MediatR;

namespace NetHub.Application.Features.Public.Articles.Many;

public record GetArticlesRequest : IRequest<ArticleModel[]>;