using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NetHub.Core.Constants;

namespace NetHub.Api.Configuration;

public static class AuthorizationExtensions
{
	public static void AddPoliciesAuthorization(this IServiceCollection services)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy(Policies.HasMasterPermission, policy => policy.RequireClaim(Permissions.Master));
		
			options.AddReadPolicy(Policies.HasReadUsersPermission, Permissions.ReadUsers);
			options.AddManagePolicy(Policies.HasManageUsersPermission, Permissions.ManageUsers);
		
			options.AddReadPolicy(Policies.HasReadRolesPermission, Permissions.ReadRoles);
			options.AddManagePolicy(Policies.HasManageRolesPermission, Permissions.ManageRoles);
		});
	}


	public static void AddReadPolicy(this AuthorizationOptions options, string policy, string permission) =>
			options.AddPolicy(policy, p => p.RequireAssertion(context =>
					context.User.HasReadPermission(permission) ||
					context.User.HasManagePermission(permission)));

	public static void AddManagePolicy(this AuthorizationOptions options, string policy, string permission) =>
			options.AddPolicy(policy, p => p.RequireAssertion(context => context.User.HasManagePermission(permission)));

	private static bool HasReadPermission(this ClaimsPrincipal user, string permission) => user.HasClaim(Claims.Permission, permission);
	private static bool HasManagePermission(this ClaimsPrincipal user, string permission) => user.HasClaim(Claims.Permission, permission);
}