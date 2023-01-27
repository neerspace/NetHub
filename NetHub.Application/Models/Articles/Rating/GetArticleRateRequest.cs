using MediatR;
using NetHub.Application.Models.Articles.Localizations;

namespace NetHub.Application.Models.Articles.Rating;

public sealed record GetArticleRateRequest(long ArticleId) : IRequest<RatingModel>;