using NetHub.Data.SqlServer.Enums;

namespace NetHub.Models.Articles.Rating;

public sealed record RateArticleRequest(long Id, Vote Vote);