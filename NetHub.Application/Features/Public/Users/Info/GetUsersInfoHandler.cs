using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Users.Info;

public class GetUsersInfoHandler : DbHandler<GetUsersInfoRequest, UserDto[]>
{
	public GetUsersInfoHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<UserDto[]> Handle(GetUsersInfoRequest request)
	{
		var users = await Database.Set<User>().Where(u => request.Ids.Contains(u.Id)).ToArrayAsync();

		return users.Select(u => u.Adapt<UserDto>()).ToArray();
	}
}