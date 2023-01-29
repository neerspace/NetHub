using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Extensions;

namespace NetHub.Shared.Api;

public class PermissionsMetadata
{
    public static readonly PermissionInfoAttribute[] AllPermissions = Enum.GetValues<Permission>()
        .Select(p => p.GetPermissionInfo()).ToArray();

    public static readonly string[] Keys = AllPermissions.Select(p => p.Key).ToArray();

    public static readonly string MasterPermission = Permission.Master.GetPermissionInfo().Key;
    public static readonly string AdminPermission = Permission.Admin.GetPermissionInfo().Key;
}