using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.ToggleSaving;

public record ToggleArticleSaveRequest(long ArticleId, string LanguageCode) : IRequest;

public class ToggleArticleSaveValidator : AbstractValidator<ToggleArticleSaveRequest>
{
	public ToggleArticleSaveValidator()
	{
		RuleFor(r => r.ArticleId).NotNull().NotEmpty().WithMessage("ArticleId is required");
		RuleFor(r => r.LanguageCode).NotNull().NotEmpty().WithMessage("LanguageCode is required");
	}
}