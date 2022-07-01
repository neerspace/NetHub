using MediatR;
using NetHub.Application.Features.Public.Articles.Localizations.Ratings.Rate;

namespace NetHub.Application.Features.Public.Articles.Localizations.Ratings.Get;

public record GetLocalizationRateRequest(long ArticleId, string Code) : IRequest<RatingModel>;