using FluentValidation;
using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.Info;

public sealed record GetUsersInfoRequest(long[] Ids) : IRequest<UserDto[]>;

internal sealed class GetUsersInfoValidator : AbstractValidator<GetUsersInfoRequest>
{
    public GetUsersInfoValidator()
    {
        RuleFor(r => r.Ids).NotNull().WithMessage("Must be provided at least one user Id");
    }
}