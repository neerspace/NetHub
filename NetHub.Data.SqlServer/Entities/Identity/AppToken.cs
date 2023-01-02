using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppToken : IdentityUserToken<long>, ICreatableEntity, IEntity
{
    public long? DeviceId { get; set; }
    public DateTime Created { get; init; }


    public AppDevice? Device { get; set; }
    public AppUser? User { get; set; }
}