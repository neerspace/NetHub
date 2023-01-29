namespace NetHub.Shared.Models.Jwt;

public record struct JwtToken(string Token, DateTimeOffset Expires);