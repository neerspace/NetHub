using FluentValidation;
using MediatR;
using NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;

namespace NetHub.Application.Features.Public.Articles.Localizations.Many;

public record GetThreadRequest
	(string LanguageCode, int Page = 1, int PerPage = 20) : IRequest<ExtendedArticleModel[]>;

public class GetThreadValidator : AbstractValidator<GetThreadRequest>
{
	public GetThreadValidator()
	{
		RuleFor(r => r.LanguageCode).NotEmpty().NotNull().WithMessage("Language code required");
		RuleFor(r => r.Page).Must(p => p > 0).WithMessage("Page must be positive");
		RuleFor(r => r.PerPage).Must(pp => pp > 0).WithMessage("Per Page must be positive");
    }
}