using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.One;

public record GetArticleLocalizationRequest(long ArticleId, string LanguageCode) : IRequest<ArticleLocalizationModel>;