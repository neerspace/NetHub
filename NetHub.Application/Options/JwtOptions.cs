using Microsoft.IdentityModel.Tokens;

namespace NetHub.Application.Options;

public class JwtOptions
{
	public SecurityKey? Secret { get; set; }
	public TimeSpan AccessTokenLifetime { get; set; }
	public TimeSpan RefreshTokenLifetime { get; set; }
}