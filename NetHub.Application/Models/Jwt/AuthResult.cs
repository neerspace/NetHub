namespace NetHub.Application.Models.Jwt;

public sealed record AuthResult
{
    /// <example>aspadmin</example>
    public string Username { get; init; } = default!;

    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; }

    /// <example>[JWT]</example>
    public string Token { get; init; } = default!;

    public DateTimeOffset TokenExpires { get; init; }
    public DateTimeOffset RefreshTokenExpires { get; init; }
    public string? ProfilePhotoUrl { get; set; }
}