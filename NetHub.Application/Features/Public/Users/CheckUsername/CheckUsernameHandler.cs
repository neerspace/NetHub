using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Users.CheckUsername;

public class CheckUsernameHandler : DbHandler<CheckUsernameRequest, CheckUsernameResult>
{
	public CheckUsernameHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<CheckUsernameResult> Handle(CheckUsernameRequest request)
	{
		var isExist = await Database.Set<User>().AnyAsync(u => u.UserName == request.Username);

		return new(!isExist);
	}
}