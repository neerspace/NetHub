using FluentValidation;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.User;

public sealed record GetUserArticlesRequest(
	int Page,
	int PerPage
) : IRequest<ArticleModel[]>;

internal sealed class GetUserArticlesValidator : AbstractValidator<GetUserArticlesRequest>
{
	public GetUserArticlesValidator()
	{
		RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page parameter must be greater than or equal to 1");
		RuleFor(x => x.PerPage).GreaterThanOrEqualTo(0).WithMessage("PerPage parameter must be greater than or equal to 0");
	}
}