using MediatR;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Ratings.SetRate;

public record RateArticleRequest(long ArticleId, Rating Rating) : IRequest;