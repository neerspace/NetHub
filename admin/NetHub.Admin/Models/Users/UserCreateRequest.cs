using FluentValidation;
using NetHub.Shared.Extensions;

namespace NetHub.Admin.Models.Users;

public sealed record UserCreateRequest
{
    /// <example>jurilents</example>
    public required string UserName { get; init; }

    /// <example>jurilents@tacles.net</example>
    public required string Email { get; init; }

    /// <example>Yurii</example>
    public required string FirstName { get; init; }

    /// <example>Yer.</example>
    public required string LastName { get; init; }

    public string? Password { get; init; }

    public string? Description { get; init; }
}

public sealed class UserCreateValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateValidator()
    {
        RuleFor(o => o.UserName).NotEmpty().UserName();
        RuleFor(o => o.Email).NotEmpty().EmailAddress();
        RuleFor(o => o.Description).MaximumLength(300);
    }
}