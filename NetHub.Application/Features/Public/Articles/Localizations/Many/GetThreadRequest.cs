using FluentValidation;
using MediatR;
using NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;
using NetHub.Application.Models;

namespace NetHub.Application.Features.Public.Articles.Localizations.Many;

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