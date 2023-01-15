using NeerCore.Data;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

public sealed class Language : IEntity
{
    public string Code { get; set; } = default!;
    public LocalizedString Name { get; set; }
}