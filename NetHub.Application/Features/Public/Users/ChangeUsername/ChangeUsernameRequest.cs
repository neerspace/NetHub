using FluentValidation;
using MediatR;
using NetHub.Application.Features.Public.Users.CheckUsername;

namespace NetHub.Application.Features.Public.Users.ChangeUsername;

public sealed record ChangeUsernameRequest(string Username) : IRequest;

internal sealed class ChangeUsernameValidator : AbstractValidator<CheckUsernameRequest>
{
    public ChangeUsernameValidator()
    {
        RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username not provided");
    }
}