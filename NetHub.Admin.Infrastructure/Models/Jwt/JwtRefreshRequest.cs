using FluentValidation;

namespace NetHub.Admin.Infrastructure.Models.Jwt;

public sealed class JwtRefreshRequest
{
    public required string RefreshToken { get; init; }
}

internal sealed class JwtRefreshRequestValidator : AbstractValidator<JwtRefreshRequest>
{
    public JwtRefreshRequestValidator()
    {
        RuleFor(o => o.RefreshToken).NotEmpty();
    }
}