using Mapster;
using NetHub.Application.Models.Users;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Mappers;

public class MeMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserProfileRequest, AppUser>()
            .Map(e => e.NormalizedUserName, m => (m.UserName ?? "").ToUpper());
    }
}