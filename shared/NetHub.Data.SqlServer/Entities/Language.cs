using NeerCore.Data.Abstractions;
using NeerCore.Localization;

namespace NetHub.Data.SqlServer.Entities;

public sealed class Language : IEntity
{
    public string Code { get; set; } = default!;
    public LocalizedString Name { get; set; }
    public Guid? FlagId { get; set; }
    public byte Order { get; set; }

    public Resource? Flag { get; set; }
}