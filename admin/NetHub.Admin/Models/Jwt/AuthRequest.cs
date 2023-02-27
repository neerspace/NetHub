using FluentValidation;
using NetHub.Shared.Extensions;

namespace NetHub.Admin.Models.Jwt;

public sealed class AuthRequest
{
    public required string Login { get; init; }
    public required string? Password { get; init; }
}

internal sealed class JwtAuthRequestValidator : AbstractValidator<AuthRequest>
{
    public JwtAuthRequestValidator()
    {
        RuleFor(o => o.Login).NotEmpty();
        When(o => !string.IsNullOrEmpty(o.Login) && o.Login.Contains('@'),
                () => RuleFor(o => o.Login).EmailAddress())
            .Otherwise(() => RuleFor(o => o.Login).UserName());
        // Validate password
        RuleFor(o => o.Password).NotEmpty();
    }
}