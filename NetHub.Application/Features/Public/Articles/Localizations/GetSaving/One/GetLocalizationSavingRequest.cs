using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.GetSaving.One;

public record GetLocalizationSavingRequest(long ArticleId, string LanguageCode) : IRequest<GetLocalizationSavingResult>;

public class GetLocalizationSavingValidator : AbstractValidator<GetLocalizationSavingRequest>
{
	public GetLocalizationSavingValidator()
	{
		RuleFor(r => r.ArticleId).NotNull().NotEmpty().WithMessage("ArticleId is required");
		RuleFor(r => r.LanguageCode).NotNull().NotEmpty().WithMessage("LanguageCode is required");
	}
}