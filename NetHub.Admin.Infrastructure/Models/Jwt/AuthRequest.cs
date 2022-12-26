using FluentValidation;
using NeerCore.Application.Abstractions;
using NetHub.Application.Extensions;

namespace NetHub.Admin.Infrastructure.Models.Jwt;

public sealed record AuthRequest(
    string Login,
    string? Password
) : ICommand<AdminAuthResult>;

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