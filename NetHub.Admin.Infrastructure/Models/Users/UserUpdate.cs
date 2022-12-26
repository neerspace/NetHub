using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetHub.Application.Extensions;

namespace NetHub.Admin.Infrastructure.Models.Users;

public sealed record UserUpdate(
    [property: FromRoute(Name = "id")] long Id,
    string UserName,
    string Email
);

public sealed class UserUpdateModelValidator : AbstractValidator<UserUpdate>
{
    public UserUpdateModelValidator()
    {
        RuleFor(o => o.Id).NotEmpty();
        RuleFor(o => o.UserName).NotEmpty().UserName();
        RuleFor(o => o.Email).NotEmpty().EmailAddress();
    }
}