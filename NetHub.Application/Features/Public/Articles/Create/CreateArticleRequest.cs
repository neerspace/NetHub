using MediatR;

namespace NetHub.Application.Features.Public.Articles.Create;

public record CreateArticleRequest(string Name, string[]? Tags, string? TranslatedArticleLink) : IRequest<ArticleModel>;