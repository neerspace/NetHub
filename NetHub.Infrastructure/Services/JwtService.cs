using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Auth;
using NetHub.Application.Services;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;
using NetHub.Infrastructure.Services.Internal;

namespace NetHub.Infrastructure.Services;

[Inject]
public class JwtService : IJwtService
{
	private readonly IDatabaseContext _database;
	private readonly RefreshTokenGenerator _refreshTokenGenerator;
	private readonly AccessTokenGenerator _accessTokenGenerator;

	public JwtService(IDatabaseContext database, AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator)
	{
		_database = database;
		_refreshTokenGenerator = refreshTokenGenerator;
		_accessTokenGenerator = accessTokenGenerator;
	}

	public async Task<AuthResult> GenerateAsync(AppUser user, CancellationToken cancel)
	{
		(string? accessToken, DateTime accessTokenExpires) = await _accessTokenGenerator.GenerateAsync(user, cancel);
		(string? refreshToken, DateTime refreshTokenExpires) = await _refreshTokenGenerator.GenerateAsync(user, cancel);

		return new AuthResult
		{
			Username = user.UserName,
			Token = accessToken,
			TokenExpires = accessTokenExpires,
			RefreshToken = refreshToken,
			RefreshTokenExpires = refreshTokenExpires
		};
	}

	public async Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken cancel)
	{
		var refreshTokens = _database.Set<AppRefreshToken>();

		var token = await refreshTokens.Where(rt => rt.Token == refreshToken).Include(rt => rt.User).FirstOr404Async(cancel);
		if (!_refreshTokenGenerator.IsValid(token))
			throw new ValidationFailedException("Refresh token is invalid.");

		var result = await GenerateAsync(token.User!, cancel);
		token.User = null;

		refreshTokens.Remove(token);
		await _database.SaveChangesAsync(cancel: cancel);

		return result;
	}
}