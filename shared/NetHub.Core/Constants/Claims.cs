using System.Security.Claims;

namespace NetHub.Core.Constants;

public struct Claims
{
    public const string Id = "sid";
    public const string Username = "username";
    public const string FirstName = "firstname";
    public const string Image = "image";
    public const string Role = ClaimTypes.Role;
    public const string Email = ClaimTypes.Email;
    public const string Registered = nameof(Registered);
    public const string PhoneNumber = nameof(PhoneNumber);
    public const string Permission = nameof(Permission);
}