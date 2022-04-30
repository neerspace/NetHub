using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetHub.Application.Options;

namespace NetHub.Api.Configuration;

public static class AuthenticationExtensions
{
	public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
	{
		services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}
		).AddJwtBearer(o =>
			{
				o.RequireHttpsMetadata = false;
				// o.SaveToken = true;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = jwtOptions.Secret,
					ValidateAudience = false,
					ValidateIssuer = false
				};
			}
		);
	}
}