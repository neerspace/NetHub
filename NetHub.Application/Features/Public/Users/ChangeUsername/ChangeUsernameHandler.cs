using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Users.ChangeUsername;

public class ChangeUsernameHandler : AuthorizedHandler<ChangeUsernameRequest>
{
	public ChangeUsernameHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(ChangeUsernameRequest request)
	{
		var isExist = await Database.Set<User>().AnyAsync(u => u.UserName == request.Username);

		if (isExist)
			throw new ValidationFailedException("username", "User with such username already exists");

		var user = await UserProvider.GetUser();

		await UserManager.SetUserNameAsync(user, request.Username);

		await Database.SaveChangesAsync();

		return Unit.Value;
	}
}