using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace NetHub.Application.Options;

public sealed class JwtOptions
{
    public required SecurityKey Secret { get; set; }
    public string? Issuer { get; set; }
    public string[]? Audiences { get; set; }
    public TimeSpan AccessTokenLifetime { get; set; }
    public TimeSpan RefreshTokenLifetime { get; set; }
    public required string RefreshTokenCookieName { get; set; }


    internal sealed class Configurator : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;
        public Configurator(IConfiguration configuration) => _configuration = configuration;

        public void Configure(JwtOptions options)
        {
            var config = _configuration.GetRequiredSection("Jwt");
            config.Bind(options);
            options.Secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetValue<string>(nameof(Secret))!));
        }
    }
}