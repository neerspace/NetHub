using NeerCore.Data.Abstractions;
using NetHub.Core.Enums;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppDevice : IEntity<long>
{
    public long Id { get; set; }

    /// <summary>
    ///   255.255.255.255 => 3*4+3 = 15 length
    ///   0000:0000:0000:0000:0000:0000:0000:0000 => 8*4+7 = 39
    ///   0000:0000:0000:0000:0000:FFFF:192.168.100.228 => (ipv6)+1+(ipv4) = 29+1+15 = 45
    /// </summary>
    public string IpAddress { get; set; } = default!;

    /// <example>Windows 11</example>
    public string Platform { get; init; } = default!;

    /// <example>Firefox</example>
    public string Browser { get; init; } = default!;

    /// <example>42.0.1</example>
    public string BrowserVersion { get; init; } = default!;

    public DeviceStatus Status { get; set; }
    public short AttemptCount { get; set; }
    public DateTime? LastAttempt { get; set; }
}