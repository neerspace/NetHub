using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Ratings.Rate;

public record RateLocalizationRequest(long ArticleId, string LanguageCode, RateModel Rating) : IRequest;

public enum RateModel
{
	Up,
	Down,
	None
}