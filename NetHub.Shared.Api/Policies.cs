namespace NetHub.Shared.Api;

public static class Policies
{
    public const string HasMasterPermission = "Master";

    public const string HasReadUsersPermission = "ReadUsers";
    public const string HasManageUsersPermission = "ManageUsers";
    public const string HasReadUserPermissionsPermission = "ReadUserPermissions";
    public const string HasManageUserPermissionsPermission = "ManageUserPermissions";

    public const string HasReadRolesPermission = "ReadRoles";
    public const string HasManageRolesPermission = "ManageRoles";

    public const string HasReadLanguagesPermission = "ReadLanguages";
    public const string HasManageLanguagesPermission = "ManageLanguages";
}