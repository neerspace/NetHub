using MediatR;

namespace NetHub.Application.Models.Articles.Localizations;

public record GetArticleLocalizationRequest(long ArticleId, string LanguageCode) : IRequest<ArticleLocalizationModel>;