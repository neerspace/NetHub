using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace NetHub.Application.Options;

public class JwtOptions
{
	public SecurityKey? Secret { get; set; }
	public TimeSpan AccessTokenLifetime { get; set; }
	public TimeSpan RefreshTokenLifetime { get; set; }

	internal class Configurator : IConfigureOptions<JwtOptions>
	{
		private readonly IConfiguration _configuration;

		public Configurator(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Configure(JwtOptions options)
		{
			options.Secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));
			options.AccessTokenLifetime = TimeSpan.Parse(_configuration["Jwt:AccessTokenLifetime"]);
			options.RefreshTokenLifetime = TimeSpan.Parse(_configuration["Jwt:RefreshTokenLifetime"]);
		}
	}
}