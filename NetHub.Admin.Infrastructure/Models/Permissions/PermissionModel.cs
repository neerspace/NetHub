namespace NetHub.Admin.Infrastructure.Models.Permissions;

public sealed record PermissionModel
{
    public string Key { get; set; } = default!;
    public string? ManageKey { get; set; }
    public string DisplayName { get; set; } = default!;
    public PermissionModel[]? Children { get; set; }
}