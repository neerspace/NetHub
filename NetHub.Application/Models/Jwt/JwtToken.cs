namespace NetHub.Application.Models.Jwt;

public record struct JwtToken(string Token, DateTimeOffset Expires);