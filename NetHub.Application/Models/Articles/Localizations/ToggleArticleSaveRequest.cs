using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record ToggleArticleSaveRequest(
    [FromRoute] long ArticleId,
    [FromRoute(Name = "lang")] string LanguageCode
);

internal sealed class ToggleArticleSaveValidator : AbstractValidator<ToggleArticleSaveRequest>
{
    public ToggleArticleSaveValidator()
    {
        RuleFor(r => r.ArticleId).NotNull().NotEmpty().WithMessage("ArticleId is required");
        RuleFor(r => r.LanguageCode).NotNull().NotEmpty().WithMessage("LanguageCode is required");
    }
}