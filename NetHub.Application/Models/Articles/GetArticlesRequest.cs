using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles;

public sealed record GetArticlesRequest([FromRoute(Name = "lang")] string LanguageCode, int Page = 1, int PerPage = 20);

internal sealed class GetArticlesValidator : AbstractValidator<GetArticlesRequest>
{
    public GetArticlesValidator()
    {
        RuleFor(r => r.LanguageCode).NotEmpty();
        RuleFor(r => r.Page).Must(p => p > 0).WithMessage("Page must be positive");
    }
}