using MediatR;
using NetHub.Application.Features.Public.Articles.Localizations;

namespace NetHub.Application.Features.Public.Articles.Ratings.Get;

public sealed record GetArticleRateRequest(long ArticleId) : IRequest<RatingModel>;