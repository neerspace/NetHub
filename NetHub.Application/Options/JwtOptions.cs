using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace NetHub.Application.Options;

public class JwtOptions
{
    public SecurityKey? Secret { get; set; }
    public string? Issuer { get; set; }
    public string[]? Audiences { get; set; }
    public TimeSpan AccessTokenLifetime { get; set; }
    public TimeSpan RefreshTokenLifetime { get; set; }


    internal class Configurator : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;
        public Configurator(IConfiguration configuration) => _configuration = configuration;

        public void Configure(JwtOptions options)
        {
            var config = _configuration.GetRequiredSection("Jwt");
            options.Secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetValue<string>(nameof(Secret))));
            options.Issuer = config.GetValue<string>(nameof(Issuer));
            options.Audiences = config.GetValue<string[]>(nameof(Audiences));
            options.AccessTokenLifetime = config.GetValue<TimeSpan>(nameof(AccessTokenLifetime));
            options.RefreshTokenLifetime = config.GetValue<TimeSpan>(nameof(RefreshTokenLifetime));
        }
    }
}