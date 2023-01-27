using FluentValidation;

namespace NetHub.Application.Models.Users;

public sealed record GetUsersInfoRequest(string[] UserNames);

internal sealed class GetUsersInfoValidator : AbstractValidator<GetUsersInfoRequest>
{
    public GetUsersInfoValidator()
    {
        RuleFor(r => r.UserNames).NotNull().WithMessage("Must be provided at least one username");
    }
}