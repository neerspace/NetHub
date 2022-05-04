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
	private readonly UserManager<UserProfile> _userManager;
	public CreateUserHandler(UserManager<UserProfile> userManager) => _userManager = userManager;


	public async Task<User> Handle(CreateUserCommand command, CancellationToken cancel)
	{
		var user = new UserProfile { UserName = command.Username, Email = command.Email };

		IdentityResult? result = await _userManager.CreateAsync(user);
		if (!result.Succeeded)
			throw new ValidationFailedException("User not created.", result.ToErrorDetails());

		await _userManager.AddPasswordAsync(user, command.Password);
		await _userManager.AddToRoleAsync(user, Role.User.ToString());

		return user.Adapt<User>();
	}
}