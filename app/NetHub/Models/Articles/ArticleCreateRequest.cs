using FluentValidation;

namespace NetHub.Models.Articles;

public sealed record ArticleCreateRequest(
    string Name,
    string[]? Tags,
    string? OriginalArticleLink
);

internal sealed class ArticleCreateValidator : AbstractValidator<ArticleCreateRequest>
{
    public ArticleCreateValidator()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("Article name not provided");
        RuleFor(r => r.Tags).Must((_, s) => s?.Length >= 3).WithMessage("Must be provided at least 3 tags");
    }
}