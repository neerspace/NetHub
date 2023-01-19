using Mapster;
using NetHub.Admin.Infrastructure.Models.Users;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Infrastructure.Mappers;

public sealed class UserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AppUser, UserModel>()
            .Map(m => m.HasPassword, e => !string.IsNullOrEmpty(e.PasswordHash));
    }
}