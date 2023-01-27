using MediatR;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Models.Articles.Rating;

public sealed record RateArticleRequest(long ArticleId, Vote Vote) : IRequest;