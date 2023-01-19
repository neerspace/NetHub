using NetHub.Api.Shared.Extensions;

namespace NetHub.Api.Shared;

public class PermissionsMetadata
{
    public static readonly PermissionInfoAttribute[] AllPermissions = Enum.GetValues<Permission>()
        .Select(p => p.GetPermissionInfo()).ToArray();

    public static readonly string[] Keys = AllPermissions.Select(p => p.Key).ToArray();

    public static readonly string MasterPermission = Permission.Master.GetPermissionInfo().Key;
    public static readonly string AdminPermission = Permission.Admin.GetPermissionInfo().Key;
}