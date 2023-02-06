namespace NetHub.Models.Me;

public sealed record MeProfileUpdateRequest(string? Username, string? FirstName, string? LastName, string? MiddleName, string? Description);