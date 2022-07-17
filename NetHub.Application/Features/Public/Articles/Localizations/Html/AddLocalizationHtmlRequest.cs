using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Html;

public record AddLocalizationHtmlRequest : IRequest
{
	[JsonIgnore] public long ArticleId { get; set; }
	[JsonIgnore] public string LanguageCode { get; set; } = default!;
	public string Html { get; set; } = default!;
}

public class AddLocalizationHtmlValidator : AbstractValidator<AddLocalizationHtmlRequest>
{
	public AddLocalizationHtmlValidator()
	{
		RuleFor(r => r.Html)
			.NotNull()
			.NotEmpty()
			.MinimumLength(500)
			.WithMessage("Minimum Article length is 500 symbols");
	}
}