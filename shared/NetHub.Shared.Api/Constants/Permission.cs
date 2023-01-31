namespace NetHub.Shared.Api.Constants;

/// <summary>
/// Required permission value pattern:
/// <code>[area].[grp].[subgrp].[mod]</code>
/// <br/>
/// <b>[area]</b> – global application area (admin/client/etc)
/// <br/>
/// <b>[grp]</b> – group inside the area (users/roles/etc)
/// <br/>
/// <b>[subgrp]</b> – sub-group of group (user-claims/article-comments/etc)
/// <br/>
/// <b>[mod]</b> – group access modifier (read-only or full manage)
/// <br/>
/// <br/>
///
/// Allowed <b>[mod]</b> values:
/// <list type="bullet">
/// <item>
///     <term>r</term><description> – grants a read-only access</description>
/// </item>
/// <item>
///     <term>m</term><description> – grants a full manage access</description>
/// </item>
/// </list>
/// </summary>
public enum Permission
{
    [PermissionInfo("*", "Full")]
    Master = 1,

    [PermissionInfo("mt", "Admin")]
    Admin = 2,

    [PermissionInfo("mt.usr", "Users")]
    ReadUsers = 10,

    [PermissionInfo("mt.usr+", "Users")]
    ManageUsers = 11,

    [PermissionInfo("mt.usr.pem", "User Permissions")]
    ReadUserPermissions = 12,

    [PermissionInfo("mt.usr.pem+", "User Permissions")]
    ManageUserPermissions = 13,

    [PermissionInfo("mt.rol", "Roles")]
    ReadRoles = 20,

    [PermissionInfo("mt.rol+", "Roles")]
    ManageRoles = 21,

    [PermissionInfo("mt.lng", "Languages")]
    ReadLanguages = 30,

    [PermissionInfo("mt.lng+", "Languages")]
    ManageLanguages = 31,
}

[AttributeUsage(AttributeTargets.Field)]
public sealed class PermissionInfoAttribute : Attribute
{
    public string Key { get; set; }
    public string DisplayName { get; set; }

    public PermissionInfoAttribute(string key, string displayName)
    {
        Key = key;
        DisplayName = displayName;
    }
}