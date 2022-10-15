using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetHub.Application.Models;
using NetHub.Application.Options;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.Constants;
using NetHub.Core.DependencyInjection;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Infrastructure.Services.Internal;

[Inject]
public class AccessTokenGenerator
{
	private readonly IDatabaseContext _database;
	private readonly JwtOptions _options;

	public AccessTokenGenerator(IOptions<JwtOptions> optionsAccessor, IDatabaseContext database)
	{
		_database = database;
		_options = optionsAccessor.Value;
	}

	public async Task<JwtToken> GenerateAsync(User user, CancellationToken cancel = default)
	{
		DateTime expires = DateTime.UtcNow.Add(_options.AccessTokenLifetime);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(await GetUserClaimsAsync(user, cancel)),
			Expires = expires,
			SigningCredentials = new SigningCredentials(_options.Secret, SecurityAlgorithms.HmacSha256Signature),
			IssuedAt = DateTime.UtcNow,
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		SecurityToken jwt = tokenHandler.CreateToken(tokenDescriptor);

		return new JwtToken(tokenHandler.WriteToken(jwt), expires);
	}

	private async Task<IEnumerable<Claim>> GetUserClaimsAsync(User user, CancellationToken cancel)
	{
		var claims = new List<Claim>
		{
			new(Claims.Id, user.Id.ToString()),
			new(Claims.Username, user.UserName),
			new(Claims.Image, user.ProfilePhotoLink ?? "")
		};

		IEnumerable<string> roles = await GetUserRolesAsync(user.Id, cancel);

		claims.AddRange(roles.Select(role => new Claim(Claims.Role, role)));

		claims.AddRange(await GetUserClaimsAsync(user.Id, cancel));
		return claims;
	}

	public async Task<IEnumerable<Claim>> GetUserClaimsAsync(long userId, CancellationToken cancel)
	{
		// TODO: mb smth not works
		List<Claim> claims = await (from u in _database.Set<IdentityUserClaim<long>>()
				where u.UserId == userId
				select new Claim(u.ClaimType, u.ClaimValue ?? "null"))
			.ToListAsync(cancel);

		claims.AddRange(await _database.Set<IdentityUserRole<long>>()
			.Where(e => e.UserId == userId)
			.Join(_database.Set<IdentityRoleClaim<long>>(), ur => ur.RoleId, rc => rc.RoleId, (_, rc) => rc)
			.Select(e => new Claim(e.ClaimType, e.ClaimValue ?? "null"))
			.ToListAsync(cancel));

		return claims;
	}

	public Task<List<string>> GetUserRolesAsync(long userId, CancellationToken cancel)
	{
		return (from u in _database.Set<IdentityUserRole<long>>()
			where u.UserId == userId
			join r in _database.Set<AppRole>() on u.RoleId equals r.Id
			select r.Name).ToListAsync(cancel);
	}
}