using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Users.Sso;

public class SsoEnterHandler : DbHandler<SsoEnterRequest, (AuthResult, string)>
{
	private readonly UserManager<User> _userManager;
	private readonly IAuthValidator _validator;
	private readonly IJwtService _jwtService;

	public SsoEnterHandler(IServiceProvider serviceProvider, UserManager<User> userManager,
		IAuthValidator validator, IJwtService jwtService) : base(serviceProvider)
	{
		_userManager = userManager;
		_validator = validator;
		_jwtService = jwtService;
	}

	// protected override async Task<(AuthModel, string)> Handle(SsoEnterRequest request)
	protected override async Task<(AuthResult, string)> Handle(SsoEnterRequest request)
	{
		var loginInfo = await GetUserLoginInfo(request.ProviderKey, request.Provider);
		var user = await _userManager.FindByIdAsync(loginInfo?.UserId.ToString());

		var validated = false;

		if (user is null)
		{
			user = await RegisterUser(request);
			validated = true;
		}

		await LoginUser(request, validated);

		var dto = await _jwtService.GenerateAsync(user)
			with
			{
				Id = user.Id,
				ProfilePhotoLink = user.ProfilePhotoLink,
				FirstName = user.FirstName
			};

		// dto.ProfilePhotoLink = user.ProfilePhotoLink;
		// dto.FirstName = user.FirstName;

		// return (dto.Adapt<AuthModel>(), dto.RefreshToken);
		return (dto, dto.RefreshToken);
	}

	private async Task LoginUser(SsoEnterRequest request, bool validated)
	{
		// var loginInfo = await GetUserLoginInfo(request.ProviderKey, request.Provider);
		// user ??= await _userManager.FindByIdAsync(loginInfo!.UserId.ToString());

		// var userProviders = await _userManager.GetLoginsAsync(user);
		//
		// if (userProviders.All(up => up.ProviderDisplayName.ToEnum<ProviderType>() != request.Provider))
		// 	throw new ValidationFailedException($"Login by {request.Provider} not supported for this user");

		if (!validated)
			await ValidateUser(request);

		// if (user is null)
		// throw new ValidationFailedException("Username", "No such User with provided username");

		// return user;
	}


	private async Task<User> RegisterUser(SsoEnterRequest request)
	{
		await ValidateUser(request);

		var user = new User
		{
			UserName = request.Username,
			FirstName = request.FirstName!,
			LastName = request.LastName,
			MiddleName = request.MiddleName,
			Email = request.Email!,
			ProfilePhotoLink = request.ProfilePhotoLink,
			EmailConfirmed = request.Provider is not ProviderType.Telegram
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

	private async Task ValidateUser(SsoEnterRequest request)
	{
		if (request.ProviderMetadata is not {Count: > 0})
			throw new ValidationFailedException("Metadata not provided");

		var isValid = await _validator.ValidateAsync(request, SsoType.Login);

		if (!isValid)
			throw new ValidationFailedException("Provided invalid data");
	}

	private async Task<IdentityUserLogin<long>?> GetUserLoginInfo(string key, ProviderType provider)
	{
		return await Database.Set<IdentityUserLogin<long>>()
			.SingleOrDefaultAsync(info =>
				info.ProviderKey == key &&
				info.LoginProvider == provider.ToString().ToLower());
	}
}