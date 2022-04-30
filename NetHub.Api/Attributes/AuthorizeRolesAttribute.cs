using Microsoft.AspNetCore.Authorization;
using NetHub.Core.Constants;

namespace NetHub.Api.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params Role[] allowedRoles)
    {
        var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(x));
        Roles = string.Join(",", allowedRolesAsStrings);
    }
}