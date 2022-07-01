// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Options;
// using NetHub.Application.Options;
// using NetHub.Application.Tools;
// using NetHub.Data.SqlServer.Entities;
//
// namespace NetHub.Application.Features.Public.Users.Sso;
//
// public class SsoEnterHandler : DbHandler<SsoEnterRequest, (AuthModel, string)>
// {
// 	private readonly UserManager<User> _userManager;
// 	private readonly TelegramOptions _telegramOptions;
//
// 	public SsoEnterHandler(IServiceProvider serviceProvider, UserManager<User> userManager,
// 		IOptions<TelegramOptions> telegramOptionsAccessor) : base(serviceProvider)
// 	{
// 		_userManager = userManager;
// 		_telegramOptions = telegramOptionsAccessor.Value;
// 	}
//
// 	protected override async Task<(AuthModel, string)> Handle(SsoEnterRequest request)
// 	{
// 		var user = await _userManager.FindByEmailAsync(request.Email);
//
// 		if (user is null)
// 			await RegisterUser(request);
// 		await LoginUser();
//
//
// 		var res = await _userManager.CreateAsync(user, request.Password);
// 	}
//
// 	private async Task RegisterUser(SsoEnterRequest request)
// 	{
// 		var user = new User
// 		{
// 			FirstName = request.FirstName,
// 			LastName = request.LastName,
// 			MiddleName = request.MiddleName,
// 			Email = request.Email,
// 			UserName = request.Username,
// 			EmailConfirmed = true
// 		};
//
// 		bool isValid;
// 		switch (request.Provider)
// 		{
// 			case ProviderType.Telegram:
// 				isValid = await CheckTelegramToken(request);
// 				break;
// 			case ProviderType.Google:
// 				break;
// 			case ProviderType.GitHub:
// 				break;
// 			case ProviderType.LinkedIn:
// 				break;
// 			case ProviderType.Facebook:
// 				break;
// 			default:
// 				throw new ArgumentOutOfRangeException();
// 		}
// 	}
//
// 	private async Task<bool> CheckTelegramToken(SsoEnterRequest request)
// 	{
// 		
// 	}
// }