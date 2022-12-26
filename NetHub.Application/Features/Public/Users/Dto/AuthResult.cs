namespace NetHub.Application.Features.Public.Users.Dto;

public sealed record AuthResult
{
    /// <example>aspadmin</example>
    public string Username { get; init; } = default!;

    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; }

    /// <example>[JWT]</example>
    public string Token { get; init; } = default!;

    public DateTime TokenExpires { get; init; }
    public string? ProfilePhotoUrl { get; set; }
}