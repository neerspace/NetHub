using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppToken : IdentityUserToken<long>, IEntity
{
    public long? DeviceId { get; set; }
    public DateTimeOffset Created { get; init; }


    public AppDevice? Device { get; set; }
    public AppUser? User { get; set; }
}