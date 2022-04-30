using MediatR;
using Microsoft.AspNetCore.Identity;
using NetHub.Application.Services;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Auth.Complete;

internal class AuthCompleteHandler : IRequestHandler<AuthCompleteCommand, AuthResult>
{
	private readonly IJwtService _jwtService;
	private readonly IDatabaseContext _database;
	private readonly SignInManager<AppUser> _signInManager;

	public AuthCompleteHandler(IJwtService jwtService, IDatabaseContext database, SignInManager<AppUser> signInManager)
	{
		_database = database;
		_jwtService = jwtService;
		_signInManager = signInManager;
	}

	public async Task<AuthResult> Handle(AuthCompleteCommand request, CancellationToken cancel)
	{
		var user = await _database.Set<AppUser>().GetByLoginAsync(request.Login, cancel: cancel);
		if (user is null) throw new NotFoundException("User not found.");

		return await PasswordAuthorizeAsync(user, request.Password!);
	}


	private async Task<AuthResult> PasswordAuthorizeAsync(AppUser user, string password)
	{
		var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
		if (!result.Succeeded) throw new ValidationFailedException("Invalid login or password.");

		return await _jwtService.GenerateAsync(user);
	}
}