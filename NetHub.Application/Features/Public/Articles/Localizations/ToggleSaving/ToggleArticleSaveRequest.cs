using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.ToggleSaving;

public record ToggleArticleSaveRequest([property: JsonIgnore] long ArticleId,
	[property: JsonIgnore] string LanguageCode, SaveArticleAction Action) : IRequest;

public enum SaveArticleAction
{
	Save,
	Delete
}

public class SaveArticleValidator : AbstractValidator<ToggleArticleSaveRequest>
{
	public SaveArticleValidator()
	{
		RuleFor(r => r.ArticleId).NotNull().NotEmpty().WithMessage("ArticleId is required");
		RuleFor(r => r.LanguageCode).NotNull().NotEmpty().WithMessage("LanguageCode is required");
		RuleFor(r => r.Action).IsInEnum().WithMessage("Choose a valid action");
	}
}