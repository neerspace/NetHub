using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record GetLocalizationSavingRequest(
    [FromRoute] long Id,
    [FromRoute(Name = "lang")] string LanguageCode
);

internal sealed class GetLocalizationSavingValidator : AbstractValidator<GetLocalizationSavingRequest>
{
    public GetLocalizationSavingValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty().WithMessage("ArticleId is required");
        RuleFor(r => r.LanguageCode).NotNull().NotEmpty().WithMessage("LanguageCode is required");
    }
}