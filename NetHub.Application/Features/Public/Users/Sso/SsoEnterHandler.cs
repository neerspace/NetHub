using Mapster;
using Microsoft.AspNetCore.Identity;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Core.Extensions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Users.Sso;

public class SsoEnterHandler : DbHandler<SsoEnterRequest, (AuthModel, string)>
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

	protected override async Task<(AuthModel, string)> Handle(SsoEnterRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);

		var validated = false;

		if (user is null)
		{
			await RegisterUser(request);
			validated = true;
		}

		var loggedUser = await LoginUser(request, user, validated);

		var dto = await _jwtService.GenerateAsync(loggedUser);

		return (dto.Adapt<AuthModel>(), dto.RefreshToken);
	}

	private async Task<User> LoginUser(SsoEnterRequest request, User? user, bool validated)
	{
		user ??= await _userManager.FindByEmailAsync(request.Email);
		var userProviders = await _userManager.GetLoginsAsync(user);

		if (userProviders.All(up => up.ProviderDisplayName.ToEnum<ProviderType>() != request.Provider))
			throw new ValidationFailedException($"Login by {request.Provider} not supported for this user");

		if (!validated)
			await ValidateUser(request);

		if (user is null)
			throw new ValidationFailedException("Username", "No such User with provided username");

		return user;
	}


	private async Task RegisterUser(SsoEnterRequest request)
	{
		await ValidateUser(request);

		var user = new User
		{
			UserName = request.Username,
			FirstName = request.FirstName,
			LastName = request.LastName,
			MiddleName = request.MiddleName,
			Email = request.Email,
			EmailConfirmed = request.Provider is not ProviderType.Telegram
		};

		var result = await _userManager.CreateAsync(user);
		if (!result.Succeeded)
			throw new ValidationFailedException(result.Errors.First().Description);

		await _userManager.AddLoginAsync(user,
			new UserLoginInfo(request.Provider.ToString(),
				Guid.NewGuid().ToString(),
				request.Provider.ToString()));
	}

	private async Task ValidateUser(SsoEnterRequest request)
	{
		if (request.ProviderMetadata is not {Count: > 0})
			throw new ValidationFailedException("Metadata not provided");

		var metadata = request.ProviderMetadata;
		if (!metadata.TryGetValue("Email", out _))
			metadata.Add("Email", request.Email);

		var isValid = await _validator.ValidateAsync(request.Provider, metadata, SsoType.Login);

		if (!isValid)
			throw new ValidationFailedException("Provided invalid data");
	}
}