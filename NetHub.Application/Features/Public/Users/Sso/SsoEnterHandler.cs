using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Users.Sso;

public sealed class SsoEnterHandler : DbHandler<SsoEnterRequest, AuthResult>
{
	private readonly UserManager<AppUser> _userManager;
	private readonly IAuthValidator _validator;
	private readonly IJwtService _jwtService;

	public SsoEnterHandler(IServiceProvider serviceProvider,
		UserManager<AppUser> userManager,
		IAuthValidator validator,
		IJwtService jwtService) : base(serviceProvider)
	{
		_userManager = userManager;
		_validator = validator;
		_jwtService = jwtService;
	}


	public override async Task<AuthResult> Handle(SsoEnterRequest request,
		CancellationToken ct)
	{
		//try to get provider login info
		var loginInfo = await GetUserLoginInfoAsync(request.ProviderKey, request.Provider, ct);
		
		//if info exists, try to get user
		var user = loginInfo is null ? null : await _userManager.FindByIdAsync(loginInfo.UserId.ToString());
		
		if (user is null)
			//is user is null - register
			user = await RegisterUserAsync(request, ct);
		else if (user is not null)
			//else - just validate
			await ValidateUserAsync(request, ct);

		//if validation passed - generate and return tokens
		return await _jwtService.GenerateAsync(user, ct);
	}

	private async Task<AppUser> RegisterUserAsync(SsoEnterRequest request,
		CancellationToken ct)
	{
		await ValidateUserAsync(request, ct);

		var user = new AppUser
		{
			UserName = request.Username,
			FirstName = request.FirstName!,
			LastName = request.LastName,
			MiddleName = request.MiddleName,
			Email = request.Email!,
			ProfilePhotoUrl = request.ProfilePhotoUrl,
			EmailConfirmed = request.Provider is not ProviderType.Telegram,
			UsernameChanges = new() {Count = 0}
		};

		var result = await _userManager.CreateAsync(user);
		if (!result.Succeeded)
			throw new ValidationFailedException(result.Errors.First().Description);

		await _userManager.AddLoginAsync(user,
			new UserLoginInfo(request.Provider.ToString().ToLower(),
				request.ProviderKey,
				null));

		return user;
	}

	private async Task ValidateUserAsync(SsoEnterRequest request,
		CancellationToken ct)
	{
		if (request.ProviderMetadata is not {Count: > 0})
			throw new ValidationFailedException("Metadata not provided");

		var isValid = await _validator.ValidateAsync(request, ct);

		if (!isValid)
			throw new ValidationFailedException("Provided invalid data");
	}

	private async Task<IdentityUserLogin<long>?> GetUserLoginInfoAsync(string key,
		ProviderType provider,
		CancellationToken ct)
	{
		return await Database.Set<AppUserLogin>()
			.SingleOrDefaultAsync(info =>
				info.ProviderKey == key &&
				info.LoginProvider == provider.ToString().ToLower(), ct);
	}
}