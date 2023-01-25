using FluentValidation;
using NeerCore.Application.Abstractions;
using NetHub.Application.Extensions;
using NetHub.Application.Models.Jwt;

namespace NetHub.Admin.Infrastructure.Models.Jwt;

public sealed class AuthRequest : ICommand<AuthResult>
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