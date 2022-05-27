using System.Security.Claims;

namespace NetHub.Core.Constants;

public struct Claims
{
	public const string Id = "sub";
	public const string UserId = "Id";
	public const string Username = nameof(Username);
	public const string Role = ClaimTypes.Role;
	public const string Email = ClaimTypes.Email;
	public const string Registered = nameof(Registered);
	public const string PhoneNumber = nameof(PhoneNumber);
}