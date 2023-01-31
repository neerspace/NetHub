using Mapster;
using NetHub.Admin.Models.Users;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Mappings;

public sealed class UserMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AppUser, UserModel>()
            .Map(m => m.HasPassword, e => !string.IsNullOrEmpty(e.PasswordHash));
    }
}