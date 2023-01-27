using FluentValidation;
using MediatR;

namespace NetHub.Application.Models.Users;

public sealed record SearchUsersRequest(string Username) : IRequest<PrivateUserDto[]>;

internal sealed class SearchUserValidator : AbstractValidator<SearchUsersRequest>
{
    public SearchUserValidator()
    {
        RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username required");
    }
}