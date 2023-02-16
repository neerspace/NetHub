using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.Articles.Localizations;

public sealed record ArticleLocalizationFilter(
    [FromRoute] string Lang,
    [FromQuery] string? ContributorUsername
);