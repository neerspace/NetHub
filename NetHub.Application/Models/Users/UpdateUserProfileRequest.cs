using FluentValidation;

namespace NetHub.Application.Models.Users;

public sealed class UpdateUserProfileRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? MiddleName { get; init; }
    public string? UserName { get; init; }
    public string? Description { get; init; }
}

internal sealed class ChangeFirstNameValidator : AbstractValidator<UpdateUserProfileRequest>
{
    public ChangeFirstNameValidator()
    {
        When(r => r.UserName != null,
            () => RuleFor(r => r.UserName).NotEmpty().Length(3, 20));

        When(r => r.FirstName != null,
            () => RuleFor(r => r.FirstName).NotEmpty());

        When(r => r.MiddleName != null,
            () => RuleFor(r => r.MiddleName).NotEmpty());

        When(r => r.LastName != null,
            () => RuleFor(r => r.LastName).NotEmpty());

        When(r => r.Description != null,
            () => RuleFor(r => r.Description).NotEmpty().MaximumLength(200));
    }
}