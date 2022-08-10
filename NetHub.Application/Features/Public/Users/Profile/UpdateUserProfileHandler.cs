using MediatR;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Users.Profile;

public class UpdateUserProfileHandler : AuthorizedHandler<UpdateUserProfileRequest>
{
	public UpdateUserProfileHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(UpdateUserProfileRequest request)
	{
		var user = await Database.Set<User>().FirstOr404Async(u => u.Id == UserProvider.GetUserId());

		if (user.FirstName != request.FirstName)
			user.FirstName = request.FirstName;

		if (user.LastName != request.LastName)
			user.LastName = request.LastName;

		if (user.MiddleName != request.MiddleName)
			user.MiddleName = request.MiddleName;

		if (user.Description != request.Description)
			user.Description = request.Description;

		await Database.SaveChangesAsync();

		return Unit.Value;
	}
}