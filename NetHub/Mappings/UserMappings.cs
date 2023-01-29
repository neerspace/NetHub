using Mapster;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;
using NetHub.Shared;
using NetHub.Shared.Abstractions;

namespace NetHub.Mappings;

public class UserMappings : IModelMappings
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserProfileRequest, AppUser>()
            .Map(e => e.NormalizedUserName, m => (m.UserName ?? "").ToUpper());
    }
}