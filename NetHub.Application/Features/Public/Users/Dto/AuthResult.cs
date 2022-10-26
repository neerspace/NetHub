namespace NetHub.Application.Features.Public.Users.Dto;

public record AuthResult
{
	/// <example>aspadmin</example>
	public string Username { get; init; } = default!;

	public string FirstName { get; set; } = default!;

	/// <example>[JWT]</example>
	public string Token { get; init; } = default!;

	public DateTime TokenExpires { get; init; }

	/// <example>[Base64]</example>
	public string RefreshToken { get; init; } = default!;

	public string? ProfilePhotoLink { get; set; }

	public DateTime RefreshTokenExpires { get; init; }
}