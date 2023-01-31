using Mapster;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;

namespace NetHub.Mappings;

public class UserMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserProfileRequest, AppUser>()
            .Map(e => e.NormalizedUserName, m => (m.UserName ?? "").ToUpper());
    }
}