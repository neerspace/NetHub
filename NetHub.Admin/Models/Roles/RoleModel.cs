namespace NetHub.Admin.Models.Roles;

public sealed class RoleModel
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string[] Permissions { get; set; }
}