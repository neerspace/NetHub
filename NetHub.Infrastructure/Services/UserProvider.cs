using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Extensions;
using NetHub.Application.Services;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Infrastructure.Services;

[Inject]
internal class UserProvider : IUserProvider
{
	private readonly IHttpContextAccessor _accessor;

	private UserManager<UserProfile> UserManager =>
		_accessor.HttpContext!.RequestServices.GetRequiredService<UserManager<UserProfile>>();


	private UserProfile? _userProfile;

	public UserProvider(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public ClaimsPrincipal User => _accessor.HttpContext!.User;

	public long GetUserId() => User.GetUserId();

	public async Task<UserProfile> GetUser()
	{
		var user = await UserManager.FindByIdAsync(GetUserId().ToString());
		if (user is null)
			throw new InternalServerException("Internal Server Error");

		return _userProfile ??= user;
	}
}