using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;

public sealed record GetSavedArticlesRequest : IRequest<ExtendedArticleModel[]>;