using Mapster;
using NetHub.Admin.Models.Users;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Abstractions;

namespace NetHub.Admin.Mappings;

public sealed class UserMappings : IModelMappings
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AppUser, UserModel>()
            .Map(m => m.HasPassword, e => !string.IsNullOrEmpty(e.PasswordHash));
    }
}