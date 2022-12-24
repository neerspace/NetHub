using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Users.Register;

public class RegisterUserHandler : DbHandler<RegisterUserRequest, UserDto>
{
	private readonly UserManager<User> _userManager;

	public RegisterUserHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_userManager = serviceProvider.GetRequiredService<UserManager<User>>();
	}

	protected override async Task<UserDto> Handle(RegisterUserRequest request)
	{
		if (request.Password != request.PasswordConfirm)
			throw new ValidationFailedException("Passwords must match");

		var user = request.Adapt<User>();

		var res = await _userManager.CreateAsync(user, request.Password);

		if (res.Errors.FirstOrDefault(e => e.Code == "DuplicateUserName") != null)
			throw new ValidationFailedException("Username", "User with provided username already exists");

		if (!res.Succeeded)
			throw new ValidationFailedException(res.Errors.Select(x => x.Description).FirstOrDefault()!);

		var registeredUser = await _userManager.FindByNameAsync(request.UserName);

		return registeredUser.Adapt<UserDto>();
	}
}