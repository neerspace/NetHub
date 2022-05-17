using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Delete;

public record DeleteArticleLocalizationRequest(long ArticleId, string LanguageCode) : IRequest;