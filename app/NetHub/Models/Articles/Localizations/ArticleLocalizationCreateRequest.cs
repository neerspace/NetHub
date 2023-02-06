using FluentValidation;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Models.Articles.Localizations;

public sealed record ArticleLocalizationCreateRequest : ArticleLocalizationQuery
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;
    public ArticleContributorModel[]? Contributors { get; set; }
}

internal sealed class CreateArticleLocalizationValidator : AbstractValidator<ArticleLocalizationCreateRequest>
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