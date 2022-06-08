using System.Security.Claims;

namespace NetHub.Core.Constants;

public struct Claims
{
	public const string Id = nameof(Id);
	public const string Username = nameof(Username);
	public const string Role = ClaimTypes.Role;
	public const string Email = ClaimTypes.Email;
	public const string Registered = nameof(Registered);
	public const string PhoneNumber = nameof(PhoneNumber);
	public const string Permission = nameof(Permission);
}