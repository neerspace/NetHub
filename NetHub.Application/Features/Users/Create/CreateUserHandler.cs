using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetHub.Application.Extensions;
using NetHub.Core.Constants;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Users.Create;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
	private readonly UserManager<AppUser> _userManager;
	public CreateUserHandler(UserManager<AppUser> userManager) => _userManager = userManager;


	public async Task<User> Handle(CreateUserCommand command, CancellationToken cancel)
	{
		var user = new AppUser { UserName = command.Username, Email = command.Email };

		IdentityResult? result = await _userManager.CreateAsync(user);
		if (!result.Succeeded)
			throw new ValidationFailedException("User not created.", result.ToErrorDetails());

		await _userManager.AddPasswordAsync(user, command.Password);
		await _userManager.AddToRoleAsync(user, Roles.User);

		return user.Adapt<User>();
	}
}