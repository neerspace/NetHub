using MediatR;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record GetSavedArticlesRequest : IRequest<ExtendedArticleModel[]>;