using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Create;

public record CreateArticleLocalizationRequest : IRequest<ArticleLocalizationModel>
{
	[JsonIgnore] public long ArticleId { get; set; }
	[JsonIgnore] public string LanguageCode { get; set; } = default!;
	public string Title { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Html { get; set; } = default!;
	public ArticleContributorModel[]? Contributors { get; set; }
}

public class CreateArticleLocalizationValidator : AbstractValidator<CreateArticleLocalizationRequest>
{
	public CreateArticleLocalizationValidator()
	{
		RuleFor(r => r.Title).NotNull().NotEmpty().WithMessage("Title not provided");
		RuleFor(r => r.Description).NotNull().NotEmpty().WithMessage("Description not provided");
		RuleFor(r => r.Html).NotNull().NotEmpty()
			// .MinimumLength(500)
			.WithMessage("Article body not provided");
	}
}