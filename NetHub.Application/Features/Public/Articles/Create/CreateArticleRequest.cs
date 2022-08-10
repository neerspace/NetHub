using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Create;

public record CreateArticleRequest(string Name, string[]? Tags, string? TranslatedArticleLink) : IRequest<ArticleModel>;

public class CreateArticleValidator : AbstractValidator<CreateArticleRequest>
{
	public CreateArticleValidator()
	{
		RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("Article name not provided");
	}
}