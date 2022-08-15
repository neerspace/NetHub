using MediatR;
using NetHub.Application.Features.Public.Articles.Localizations;

namespace NetHub.Application.Features.Public.Articles.Ratings.Get;

public record GetArticleRateRequest(long ArticleId) : IRequest<RatingModel>;