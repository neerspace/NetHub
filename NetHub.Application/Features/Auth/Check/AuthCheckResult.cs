using NetHub.Core.Enums;

namespace NetHub.Application.Features.Auth.Check;

public record AuthCheckResult(
	bool UserExists,
	string? Username,
	AuthMethod PreferAuthMethod,
	IEnumerable<AuthMethod> AllowedAuthMethod
);