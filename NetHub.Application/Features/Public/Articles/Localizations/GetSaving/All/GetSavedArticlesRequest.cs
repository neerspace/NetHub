using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;

public record GetSavedArticlesRequest() : IRequest<ExtendedArticleModel[]>;