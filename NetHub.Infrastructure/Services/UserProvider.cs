using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Extensions;
using NetHub.Application.Interfaces;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Infrastructure.Services;

[Inject]
internal class UserProvider : IUserProvider
{
	private readonly IHttpContextAccessor _accessor;

	private UserManager<User> UserManager =>
		_accessor.HttpContext!.RequestServices.GetRequiredService<UserManager<User>>();


	private User? _userProfile;

	public UserProvider(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public ClaimsPrincipal User => _accessor.HttpContext!.User;

	public long GetUserId() => User.GetUserId();

	public async Task<User> GetUser()
	{
		var user = await UserManager.FindByIdAsync(GetUserId().ToString());
		if (user is null)
			throw new InternalServerException();

		return _userProfile ??= user;
	}
}