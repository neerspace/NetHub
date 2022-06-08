using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetHub.Application.Models;
using NetHub.Application.Options;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.DependencyInjection;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Infrastructure.Services.Internal;

[Inject]
public class RefreshTokenGenerator
{
	private readonly JwtOptions _options;
	private readonly IDatabaseContext _database;
	private readonly DbSet<RefreshToken> _refreshTokensSet;
	private readonly IHttpContextAccessor _httpContextAccessor;

	private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

	public RefreshTokenGenerator(IOptions<JwtOptions> optionsAccessor, IHttpContextAccessor httpContextAccessor,
		IDatabaseContext database)
	{
		_database = database;
		_options = optionsAccessor.Value;
		_httpContextAccessor = httpContextAccessor;
		_refreshTokensSet = _database.Set<RefreshToken>();
	}

	public async Task<JwtToken> GenerateAsync(UserProfile user, CancellationToken cancel = default)
	{
		DateTime expires = DateTime.UtcNow.Add(_options.RefreshTokenLifetime);
		string token = GenerateRandomToken();

		_refreshTokensSet.Add(new RefreshToken
		{
			Value = token,
			UserId = user.Id
		});

		await _database.SaveChangesAsync(cancel: cancel);

		return new JwtToken(token, expires);
	}

	public bool IsValid(RefreshToken token)
	{
		return !string.IsNullOrEmpty(token.Value)
		       && token.Created.Add(_options.RefreshTokenLifetime) > DateTime.UtcNow;
	}

	private static string GenerateRandomToken()
	{
		var randomNumber = new byte[64];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		// To base64 without ending '=='
		string base64 = Convert.ToBase64String(randomNumber)[..^2];
		return base64.Replace('/', '-').Replace('+', '_');
	}
}