using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using NetHub.Core.Constants;
using NetHub.Shared.Api.Constants;

namespace NetHub.Shared.Api.Extensions;

internal static class PolicyAuthorizationOptionsExtensions
{
    private static readonly Type s_permissionType = typeof(Permission);

    internal static PermissionInfoAttribute GetPermissionInfo(this Permission permission) =>
        s_permissionType
            .GetMember(permission.ToString()).Single()
            .GetCustomAttribute<PermissionInfoAttribute>()!;


    internal static void AddReadManagePolicy(
        this AuthorizationOptions options,
        string readPolicy,
        Permission readPermission,
        string managePolicy,
        Permission managePermission)
    {
        var managePermissionName = managePermission.GetPermissionInfo().Key;
        var readPermissionName = readPermission.GetPermissionInfo().Key;

        // READ
        options.AddPolicy(readPolicy,
            p => p.RequireClaim(Claims.Permissions,
                readPermissionName,
                managePermissionName,
                PermissionsMetadata.MasterPermission));

        // MANAGE
        options.AddPolicy(managePolicy,
            p => p.RequireClaim(Claims.Permissions,
                managePermissionName,
                PermissionsMetadata.MasterPermission));
    }

    internal static void AddReadManagePolicy(
        this AuthorizationOptions options,
        string readPolicy,
        IEnumerable<Permission> readPermissions,
        string managePolicy,
        IEnumerable<Permission> managePermissions)
    {
        var managePermissionNames = managePermissions
            .Select(p => p.GetPermissionInfo().Key)
            .Append(PermissionsMetadata.MasterPermission).ToArray();
        var readPermissionNames = readPermissions
            .Select(p => p.GetPermissionInfo().Key)
            .Union(managePermissionNames).ToArray();

        // READ
        options.AddPolicy(readPolicy,
            p => p.RequireClaim(Claims.Permissions, readPermissionNames));

        // MANAGE
        options.AddPolicy(managePolicy,
            p => p.RequireClaim(Claims.Permissions, managePermissionNames));
    }
}