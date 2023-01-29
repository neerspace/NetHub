using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record ArticlesFilterRequest([FromRoute(Name = "lang")] string LanguageCode) : FilterRequest;
// (string LanguageCode, string? Filters, string Sorting, int Page = 1, int Limit = 20) : IRequest<ExtendedArticleModel[]>, Filter;

internal sealed class GetThreadValidator : AbstractValidator<ArticlesFilterRequest>
{
    public GetThreadValidator()
    {
        RuleFor(r => r.Filters).Must(f => f.Contains("languageCode"))
            .NotNull().WithMessage("Language code required");
    }
}