using Mapster;
using MediatR;
using NetHub.Core.Abstractions.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Users.GetByUsername;

internal class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, User>
{
	private readonly IDatabaseContext _database;
	public GetUserByUsernameHandler(IDatabaseContext database) => _database = database;


	public async Task<User> Handle(GetUserByUsernameQuery query, CancellationToken cancel)
	{
		var user = await _database.Set<UserProfile>().Where(u => u.UserName == query.Username).FirstOr404Async(cancel);
		return user.Adapt<User>();
	}
}