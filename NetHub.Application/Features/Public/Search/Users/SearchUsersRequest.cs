using FluentValidation;
using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Search.Users;

public record SearchUsersRequest(string Username): IRequest<PrivateUserDto[]>;

public class SearchUserValidator : AbstractValidator<SearchUsersRequest>
{
	public SearchUserValidator()
	{
		RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username required");
	}
}