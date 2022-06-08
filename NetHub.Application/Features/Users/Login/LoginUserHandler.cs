﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Users.Dto;
using NetHub.Application.Services;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Users.Login;

public class LoginUserHandler : DbHandler<LoginUserRequest, AuthResult>
{
	private readonly IJwtService _jwtService;
	private readonly UserManager<UserProfile> _userManager;
	private readonly SignInManager<UserProfile> _signInManager;

	public LoginUserHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_jwtService = serviceProvider.GetRequiredService<IJwtService>();
		_userManager = serviceProvider.GetRequiredService<UserManager<UserProfile>>();
		_signInManager = serviceProvider.GetRequiredService<SignInManager<UserProfile>>();
	}

	protected override async Task<AuthResult> Handle(LoginUserRequest request)
	{
		var user = await _userManager.FindByNameAsync(request.Username);
		if (user is null)
			throw new ValidationFailedException("Username", "No such User with provided username");

		var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
		if (!result.Succeeded)
			throw new ValidationFailedException("Password", "Invalid password");

		return await _jwtService.GenerateAsync(user);
	}
}