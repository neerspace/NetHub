using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Search.Users;

public class SearchUsersHandler : DbHandler<SearchUsersRequest, PrivateUserDto[]>
{
	public SearchUsersHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override Task<PrivateUserDto[]> Handle(SearchUsersRequest request)
	{
		var result = Database.Set<User>()
			.Where(u => u.NormalizedUserName.Contains(request.Username.ToUpper()))
			.ProjectToType<PrivateUserDto>()
			.ToArrayAsync();

		return result;
	}
}