using Microsoft.AspNetCore.Mvc;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Models.Articles.Rating;

public sealed record ArticleVoteUpdateRequest([FromRoute] long Id, [FromQuery] Vote Vote);