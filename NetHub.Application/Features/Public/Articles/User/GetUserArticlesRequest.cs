using MediatR;

namespace NetHub.Application.Features.Public.Articles.User;

public record GetUserArticlesRequest(int Page, int PerPage) : IRequest<ArticleModel[]>;