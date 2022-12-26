using FluentValidation;
using NetHub.Application.Extensions;

namespace NetHub.Admin.Infrastructure.Models.Jwt;

public sealed record AuthVerifyRequest
{
    /// <example>jurilents</example>
    public required string Login { get; init; }
}

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