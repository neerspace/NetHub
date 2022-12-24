using System.Runtime.Serialization;

namespace NetHub.Admin;

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
    [EnumMember(Value = "-")] None = 0,
    [EnumMember(Value = "*")] Master = 1,
    [EnumMember(Value = "mt")] Admin = 2,

    [EnumMember(Value = "mt.usr.r")] ReadUsers = 10,
    [EnumMember(Value = "mt.usr.m")] ManageUsers = 11,

    [EnumMember(Value = "mt.usr.pem.r")] ReadUserPermissions = 20,
    [EnumMember(Value = "mt.usr.pem.m")] ManageUserPermissions = 21,

    [EnumMember(Value = "mt.rol.r")] ReadRoles = 30,
    [EnumMember(Value = "mt.rol.m")] ManageRoles = 31,
}