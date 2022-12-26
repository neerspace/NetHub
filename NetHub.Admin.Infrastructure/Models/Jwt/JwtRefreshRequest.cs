using FluentValidation;

namespace NetHub.Admin.Infrastructure.Models.Jwt;

public sealed record JwtRefreshRequest(
    string RefreshToken
);

internal sealed class JwtRefreshRequestValidator : AbstractValidator<JwtRefreshRequest>
{
    public JwtRefreshRequestValidator()
    {
        RuleFor(o => o.RefreshToken).NotEmpty();
    }
}