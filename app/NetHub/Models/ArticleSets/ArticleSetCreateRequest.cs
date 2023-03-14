using FluentValidation;

namespace NetHub.Models.ArticleSets;

public sealed class ArticleSetCreateRequest
{
    public string[]? Tags { get; init; }
    public string? OriginalArticleLink { get; init; }
}

internal sealed class ArticleSetCreateValidator : AbstractValidator<ArticleSetCreateRequest>
{
    public ArticleSetCreateValidator()
    {
        RuleFor(r => r.Tags).Must((_, s) => s?.Length >= 3).WithMessage("Must be provided at least 3 tags");
    }
}