﻿using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Extensions;
using NetHub.Application.Features.Users.Dto;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Users.Me;

public class GetUserHandler : AuthorizedHandler<GetUserRequest, UserProfileDto>
{
	private readonly UserManager<UserProfile> _userManager;

	public GetUserHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_userManager = serviceProvider.GetRequiredService<UserManager<UserProfile>>();
	}

	protected override async Task<UserProfileDto> Handle(GetUserRequest request)
	{
		var userId = UserProvider.GetUserId();
		var user = await _userManager.FindByIdAsync(userId);

		return user.Adapt<UserProfileDto>();
	}
}