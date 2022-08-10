namespace NetHub.Application.Features.Public.Users;

public class AuthModel
{
	/// <example>aspadmin</example>
	public string Username { get; init; } = default!;

	/// <example>[JWT]</example>
	public string Token { get; init; } = default!;
	
	public DateTime TokenExpires { get; init; }

	public DateTime RefreshTokenExpires { get; init; }
}