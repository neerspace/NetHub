using FluentValidation;
using MediatR;
using NetHub.Application.Models.Jwt;

namespace NetHub.Application.Features.Public.Users.CheckUserExists;

public sealed record CheckUserExistsRequest(string Key, ProviderType Provider) : IRequest<CheckUserExistsResult>;

internal sealed class CheckUserExistsValidator : AbstractValidator<CheckUserExistsRequest>
{
    public CheckUserExistsValidator()
    {
        RuleFor(r => r.Key).NotNull().NotEmpty().WithMessage("Provider key required");
        RuleFor(r => r.Provider).IsInEnum().WithMessage("Wrong provider type");
    }
}