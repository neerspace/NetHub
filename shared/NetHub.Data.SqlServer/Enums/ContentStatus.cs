using System.Runtime.Serialization;

namespace NetHub.Data.SqlServer.Enums;

public enum ContentStatus : byte
{
    [EnumMember(Value = "Draft")]
    Draft,

    [EnumMember(Value = "Pending")]
    Pending,

    [EnumMember(Value = "Published")]
    Published,

    [EnumMember(Value = "Banned")]
    Banned,
}