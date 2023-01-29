using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record CreateArticleLocalizationRequest
{
    [FromRoute]
    public long ArticleId { get; set; }

    [FromRoute(Name = "lang")]
    public string LanguageCode { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;
    public ArticleContributorModel[]? Contributors { get; set; }
}

internal sealed class CreateArticleLocalizationValidator : AbstractValidator<CreateArticleLocalizationRequest>
{
    public CreateArticleLocalizationValidator()
    {
        RuleFor(r => r.Title).NotNull().NotEmpty().WithMessage("Title not provided");
        RuleFor(r => r.Description).NotNull().NotEmpty().WithMessage("Description not provided");
        RuleFor(r => r.Html).NotNull().NotEmpty()
            // .MinimumLength(500)
            .WithMessage("Article body not provided");
    }
}