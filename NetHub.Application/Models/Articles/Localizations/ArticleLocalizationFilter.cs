using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record ArticleLocalizationFilter([FromRoute(Name = "lang")] string LanguageCode) : FilterRequest;

internal sealed class ArticleLocalizationFilterValidator : AbstractValidator<ArticleLocalizationFilter>
{
    public ArticleLocalizationFilterValidator()
    {
        RuleFor(r => r.Filters).Must(f => f.Contains("languageCode"))
            .NotNull().WithMessage("Language code required");
    }
}