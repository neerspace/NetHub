using FluentValidation;

namespace NetHub.Application.Models.Articles;

public sealed record CreateArticleRequest(
    string Name,
    string[]? Tags,
    string? OriginalArticleLink
);

internal sealed class CreateArticleValidator : AbstractValidator<CreateArticleRequest>
{
    public CreateArticleValidator()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("Article name not provided");
        RuleFor(r => r.Tags).Must((_, s) => s?.Length >= 3).WithMessage("Must be provided at least 3 tags");
    }
}