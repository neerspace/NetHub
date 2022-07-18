using System.ComponentModel.DataAnnotations;
using System.Data;
using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.GetMany;

public record GetArticlesRequest([Required] string Code, int Page = 1, int PerPage = 20) : IRequest<ArticleModel[]>;

public class GetArticlesValidator : AbstractValidator<GetArticlesRequest>
{
	public GetArticlesValidator()
	{
		RuleFor(r => r.Page).Must(p => p > 0).WithMessage("Page must be positive");
	}
}