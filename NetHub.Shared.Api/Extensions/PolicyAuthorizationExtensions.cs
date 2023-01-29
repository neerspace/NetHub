using Microsoft.Extensions.DependencyInjection;
using NetHub.Shared.Api.Constants;

namespace NetHub.Shared.Api.Extensions;

public static class PolicyAuthorizationExtensions
{
    public static void AddPoliciesAuthorization(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            // /users
            options.AddReadManagePolicy(
                readPolicy: Policies.HasReadUsersPermission,
                readPermission: Permission.ReadUsers,
                managePolicy: Policies.HasManageUsersPermission,
                managePermission: Permission.ManageUsers);

            // /users/permissions
            options.AddReadManagePolicy(
                readPolicy: Policies.HasReadUserPermissionsPermission,
                readPermission: Permission.ReadUserPermissions,
                managePolicy: Policies.HasManageUserPermissionsPermission,
                managePermission: Permission.ManageUserPermissions);

            // /roles
            options.AddReadManagePolicy(
                readPolicy: Policies.HasReadRolesPermission,
                readPermission: Permission.ReadRoles,
                managePolicy: Policies.HasManageRolesPermission,
                managePermission: Permission.ManageRoles);

            // /languages
            options.AddReadManagePolicy(
                readPolicy: Policies.HasReadLanguagesPermission,
                readPermission: Permission.ReadLanguages,
                managePolicy: Policies.HasManageLanguagesPermission,
                managePermission: Permission.ManageLanguages);
        });
}