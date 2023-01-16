using Mapster;
using NetHub.Admin.Infrastructure.Models.Roles;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Infrastructure.Mappers;

public class RoleMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AppRole, RoleModel>()
            .Map(m => m.Name, e => e.NormalizedName)
            .Map(m => m.Permissions, e => (e.RoleClaims ?? Array.Empty<AppRoleClaim>())
                .Where(rc => rc.ClaimType == Claims.Permissions)
                .Select(rc => rc.ClaimValue).ToArray());

        config.NewConfig<RoleModel, AppRole>()
            .Map(m => m.Name, e => e.Name)
            .Map(m => m.NormalizedName, e => e.Name)
            .Map(m => m.RoleClaims, e => e.Permissions
                .Select(p => new AppRoleClaim
                {
                    RoleId = e.Id,
                    ClaimType = Claims.Permissions,
                    ClaimValue = p
                }).ToList());
    }
}