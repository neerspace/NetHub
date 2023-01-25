using MediatR;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Ratings.SetRate;

public sealed record RateArticleRequest(long ArticleId, Vote Vote) : IRequest;