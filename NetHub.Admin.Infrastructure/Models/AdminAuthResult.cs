namespace NetHub.Admin.Infrastructure.Models;

public sealed record AdminAuthResult
{
    public required string Username { get; init; } = default!;
    public required string Name { get; set; } = default!;
    public required string? ProfilePhotoUrl { get; set; }
}