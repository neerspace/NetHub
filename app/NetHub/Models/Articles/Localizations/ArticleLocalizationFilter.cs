using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetHub.Shared.Models;

namespace NetHub.Models.Articles.Localizations;

public sealed record ArticleLocalizationFilter([FromRoute] string LanguageCode) : FilterRequest;

internal sealed class ArticleLocalizationFilterValidator : AbstractValidator<ArticleLocalizationFilter>
{
    public ArticleLocalizationFilterValidator()
    {
        RuleFor(r => r.Filters).Must(f => f.Contains("languageCode"))
            .NotNull().WithMessage("Language code is required");
    }
}