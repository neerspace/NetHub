using FluentValidation;
using NetHub.Application.Extensions;

namespace NetHub.Admin.Infrastructure.Models.Jwt;

public sealed record AuthVerifyRequest(
    string Login
);

internal sealed class AuthVerifyRequestValidator : AbstractValidator<AuthVerifyRequest>
{
    public AuthVerifyRequestValidator()
    {
        RuleFor(o => o.Login).NotEmpty();
        When(o => !string.IsNullOrEmpty(o.Login) && o.Login.Contains('@'),
                () => RuleFor(o => o.Login).EmailAddress())
            .Otherwise(() => RuleFor(o => o.Login).UserName());
    }
}