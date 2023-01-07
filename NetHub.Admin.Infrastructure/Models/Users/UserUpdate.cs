using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetHub.Application.Extensions;

namespace NetHub.Admin.Infrastructure.Models.Users;

public sealed class UserUpdate
{
    public long Id { get; init; }

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

public sealed class UserUpdateModelValidator : AbstractValidator<UserUpdate>
{
    public UserUpdateModelValidator()
    {
        RuleFor(o => o.Id).NotEmpty();
        RuleFor(o => o.UserName).NotEmpty().UserName();
        RuleFor(o => o.Email).NotEmpty().EmailAddress();
    }
}