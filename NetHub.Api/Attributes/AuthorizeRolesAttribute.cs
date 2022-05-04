using Microsoft.AspNetCore.Authorization;

namespace NetHub.Api.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] allowedRoles)
    {
        // var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(x));
        Roles = string.Join(",", allowedRoles);
    }
}