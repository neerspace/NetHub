using FluentValidation;
using MediatR;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record GetThreadRequest : FilterRequest, IRequest<ExtendedArticleModel[]>;
// (string LanguageCode, string? Filters, string Sorting, int Page = 1, int Limit = 20) : IRequest<ExtendedArticleModel[]>, Filter;

internal sealed class GetThreadValidator : AbstractValidator<GetThreadRequest>
{
    public GetThreadValidator()
    {
        RuleFor(r => r.Filters).Must(f => f.Contains("languageCode"))
            .NotNull().WithMessage("Language code required");
    }
}