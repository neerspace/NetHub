using MediatR;

namespace NetHub.Application.Features.Public.Articles.Ratings.Rate;

public record RateArticleRequest(long ArticleId, RateModel Rating) : IRequest;

public enum RateModel
{
	Up,
	Down,
	None
}