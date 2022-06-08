namespace NetHub.Application.Features.Users.Dto;

public record AuthResult
{
	/// <example>aspadmin</example>
	public string Username { get; init; } = default!;

	/// <example>[JWT]</example>
	public string Token { get; init; } = default!;

	public DateTime TokenExpires { get; init; }

	/// <example>[Base64]</example>
	public string RefreshToken { get; init; } = default!;

	public DateTime RefreshTokenExpires { get; init; }
}