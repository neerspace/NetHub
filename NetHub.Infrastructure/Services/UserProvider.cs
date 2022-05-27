using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NetHub.Application.Extensions;
using NetHub.Application.Services;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Infrastructure.Services;

[Inject]
public class UserProvider : IUserProvider
{
	private readonly IHttpContextAccessor _accessor;

	private UserProfile? _userProfile;

	public UserProvider(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public ClaimsPrincipal User => _accessor.HttpContext!.User;

	public long GetUserId() => User.GetUserId();

	public UserProfile GetUser() =>
		_userProfile ??= _accessor.HttpContext!.Items.TryGetValue("User", out var up)
			? (UserProfile) up!
			: throw new InternalServerException("Attempt to get user from not authorized route");
}