using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace NetHub.Admin.Infrastructure.Options;

public sealed class CookieJwtOptions
{
    public SecurityKey Secret { get; set; } = default!;
    public string? Issuer { get; set; }
    public string[]? Audiences { get; set; }
    public TokenOptions AccessToken { get; set; } = default!;
    public TokenOptions RefreshToken { get; set; } = default!;

    public sealed class TokenOptions
    {
        public TimeSpan Lifetime { get; set; }
        public string CookieName { get; set; } = default!;
    }


    internal sealed class Configurator : IConfigureOptions<CookieJwtOptions>
    {
        private readonly IConfiguration _configuration;
        public Configurator(IConfiguration configuration) => _configuration = configuration;

        public void Configure(CookieJwtOptions options)
        {
            var config = _configuration.GetRequiredSection("Jwt");
            options.Secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetValue<string>(nameof(Secret))!));
            options.Issuer = config.GetValue<string>(nameof(Issuer));
            options.Audiences = config.GetValue<string[]>(nameof(Audiences));
            options.AccessToken = config.GetSection(nameof(AccessToken)).Get<TokenOptions>()!;
            options.RefreshToken = config.GetSection(nameof(RefreshToken)).Get<TokenOptions>()!;
        }
    }
}