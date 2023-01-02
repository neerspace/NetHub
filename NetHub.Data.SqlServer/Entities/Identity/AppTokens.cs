using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppTokens : IdentityUserToken<long>, ICreatableEntity
{
    public override string Value { get; set; } = default!;

    /// <example>Windows 11</example>
    public string Device { get; init; } = default!;

    /// <example>Firefox</example>
    public string Browser { get; init; } = default!;

    /// <summary>
    ///   255.255.255.255 => 3*4+3 = 15 length
    ///   0000:0000:0000:0000:0000:0000:0000:0000 => 8*4+7 = 39
    ///   0000:0000:0000:0000:0000:FFFF:192.168.100.228 => (ipv6)+1+(ipv4) = 29+1+15 = 45
    /// </summary>
    public string IpAddress { get; init; } = default!;

    public DateTime Created { get; init; }


    public AppUser? User { get; set; }
}