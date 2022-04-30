using MediatR;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.Enums;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Auth.Check;

internal class AuthCheckHandler : IRequestHandler<AuthCheckQuery, AuthCheckResult>
{
	private readonly IDatabaseContext _database;
	
	public AuthCheckHandler(IDatabaseContext database)
	{
		_database = database;
	}


	public async Task<AuthCheckResult> Handle(AuthCheckQuery request, CancellationToken cancel)
	{
		var user = await _database.Set<AppUser>().GetByLoginAsync(request.Login, cancel: cancel);
		var authMethods = GetUserAuthMethods(user);

		return new AuthCheckResult(
			UserExists: user is not null,
			Username: user?.UserName ?? request.Login,
			PreferAuthMethod: SwitchPreferAuthMethod(user, authMethods),
			AllowedAuthMethod: authMethods
		);
	}


	private static IReadOnlyList<AuthMethod> GetUserAuthMethods(AppUser? user)
	{
		if (user is null)
			return new List<AuthMethod> { AuthMethod.Register };

		var authMethods = new List<AuthMethod>();

		if (!string.IsNullOrEmpty(user.PasswordHash))
			authMethods.Add(AuthMethod.Password);

		return authMethods;
	}

	private static AuthMethod SwitchPreferAuthMethod(AppUser? user, IReadOnlyList<AuthMethod> authMethods)
	{
		if (user is null || authMethods.Contains(AuthMethod.Register))
			return AuthMethod.Register;

		return AuthMethod.Password;
	}
}