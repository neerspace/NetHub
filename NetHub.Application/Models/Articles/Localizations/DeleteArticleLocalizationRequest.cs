using MediatR;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record DeleteArticleLocalizationRequest(long ArticleId, string LanguageCode) : IRequest;