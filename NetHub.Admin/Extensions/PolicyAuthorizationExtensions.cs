using Microsoft.AspNetCore.Authorization;

namespace NetHub.Admin.Extensions;

public static class PolicyAuthorizationExtensions
{
    private static readonly string MasterPermission = Permission.Master.GetRequiredDisplayName();

    public static void AddPoliciesAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddReadManagePolicy(
                readPolicy: Policies.HasReadUsersPermission,
                managePolicy: Policies.HasManageUsersPermission,
                readPermission: Permission.ReadUsers,
                managePermission: Permission.ManageUsers);

            options.AddReadManagePolicy(
                readPolicy: Policies.HasReadUserPermissionsPermission,
                managePolicy: Policies.HasManageUserPermissionsPermission,
                readPermission: Permission.ReadUserPermissions,
                managePermission: Permission.ManageUserPermissions);

            options.AddReadManagePolicy(
                readPolicy: Policies.HasReadRolesPermission,
                managePolicy: Policies.HasManageRolesPermission,
                readPermission: Permission.ReadRoles,
                managePermission: Permission.ManageRoles);
        });
    }

    private static void AddReadManagePolicy(this AuthorizationOptions options, string readPolicy, string managePolicy, Permission readPermission, Permission managePermission)
    {
        options.AddReadPolicy(readPolicy, readPermission, managePermission);
        options.AddManagePolicy(managePolicy, managePermission);
    }

    private static void AddManagePolicy(this AuthorizationOptions options, string policy, Permission permission) =>
        options.AddManagePolicy(policy, permission.GetRequiredDisplayName());

    private static void AddReadPolicy(this AuthorizationOptions options, string policy, Permission permission, Permission altPermission) =>
        options.AddReadPolicy(policy, permission.GetRequiredDisplayName(), altPermission.GetRequiredDisplayName());

    private static void AddReadPolicy(this AuthorizationOptions options, string policy, string permission, string altPermission) =>
        options.AddPolicy(policy, p => p.RequireClaim(AppClaims.Permissions,
            permission, altPermission, MasterPermission));

    private static void AddManagePolicy(this AuthorizationOptions options, string policy, string permission) =>
        options.AddPolicy(policy, p => p.RequireClaim(AppClaims.Permissions,
            permission, MasterPermission));
}