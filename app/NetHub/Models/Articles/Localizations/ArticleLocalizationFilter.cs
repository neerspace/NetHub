using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetHub.Shared.Models;

namespace NetHub.Models.Articles.Localizations;

public sealed record ArticleLocalizationFilter(
    [FromRoute] string Lang,
    [FromQuery] string? ContributorUsername
);

internal sealed class ArticleLocalizationFilterValidator : AbstractValidator<ArticleLocalizationFilter>
{
    public ArticleLocalizationFilterValidator() { }
}