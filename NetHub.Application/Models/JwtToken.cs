namespace NetHub.Application.Models;

public record struct JwtToken(string Token, DateTimeOffset Expires);