using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using NetHub.Core.Constants;

namespace NetHub.Api.Shared.Extensions;

internal static class PolicyAuthorizationOptionsExtensions
{
    private static readonly Type s_permissionType = typeof(Permission);
    private static readonly string s_masterPermission = Permission.Master.GetPermissionInfo().Key;

    public static PermissionInfoAttribute GetPermissionInfo(this Permission permission) =>
        s_permissionType
            .GetMember(permission.ToString()).Single()
            .GetCustomAttribute<PermissionInfoAttribute>()!;

    public static void AddReadManagePolicy(
        this AuthorizationOptions options, string readPolicy,
        string managePolicy, Permission readPermission, Permission managePermission)
    {
        options.AddReadPolicy(readPolicy, readPermission, managePermission);
        options.AddManagePolicy(managePolicy, managePermission);
    }

    private static void AddManagePolicy(this AuthorizationOptions options, string policy, Permission permission) =>
        options.AddManagePolicy(policy, permission.GetPermissionInfo().Key);

    private static void AddReadPolicy(this AuthorizationOptions options, string policy, Permission permission, Permission altPermission) =>
        options.AddReadPolicy(policy, permission.GetPermissionInfo().Key, altPermission.GetPermissionInfo().Key);

    private static void AddReadPolicy(this AuthorizationOptions options, string policy, string permission, string altPermission) =>
        options.AddPolicy(policy, p => p.RequireClaim(Claims.Permission,
            permission, altPermission, s_masterPermission));

    private static void AddManagePolicy(this AuthorizationOptions options, string policy, string permission) =>
        options.AddPolicy(policy, p => p.RequireClaim(Claims.Permission,
            permission, s_masterPermission));
}