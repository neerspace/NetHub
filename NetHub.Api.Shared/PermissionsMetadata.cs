using NeerCore.Extensions;

namespace NetHub.Api.Shared;

public class PermissionsMetadata
{
    public static readonly Permission[] Keys = Enum.GetValues<Permission>();
    public static readonly string[] Values = Enum.GetValues<Permission>().Select(p => p.GetRequiredDisplayName()).ToArray();

    public static readonly string MasterPermission = Permission.Master.GetRequiredDisplayName();
    public static readonly string AdminPermission = Permission.Admin.GetRequiredDisplayName();
}