using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Filter;

public record FilterArticleLocalizationsRequest : IRequest<ArticleLocalizationModel[]>;