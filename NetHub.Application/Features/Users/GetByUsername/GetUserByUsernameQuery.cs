using FluentValidation;
using MediatR;
using NetHub.Application.Extensions;

namespace NetHub.Application.Features.Users.GetByUsername;

public class GetUserByUsernameQuery : IRequest<User>
{
	/// <example>aspadmin</example>
	public string Username { get; init; }


	public GetUserByUsernameQuery(string username)
	{
		Username = username;
	}
}

public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
{
	public GetUserByUsernameQueryValidator()
	{
		RuleFor(o => o.Username).NotEmpty().UserName();
	}
}