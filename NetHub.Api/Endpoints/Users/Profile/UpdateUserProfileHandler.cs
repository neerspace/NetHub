using MediatR;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Users.Profile;

internal sealed class UpdateUserProfileHandler : AuthorizedHandler<UpdateUserProfileRequest>
{
    public UpdateUserProfileHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<Unit> Handle(UpdateUserProfileRequest request, CancellationToken ct)
    {
        var user = await Database.Set<AppUser>().FirstOr404Async(u => u.Id == UserProvider.UserId, ct);

        if (user.FirstName != request.FirstName)
            user.FirstName = request.FirstName;

        if (user.LastName != request.LastName)
            user.LastName = request.LastName;

        if (user.MiddleName != request.MiddleName)
            user.MiddleName = request.MiddleName;

        if (user.Description != request.Description)
            user.Description = request.Description;

        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }
}