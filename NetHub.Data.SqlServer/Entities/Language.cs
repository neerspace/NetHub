using NetHub.Core.Abstractions.Entities;

namespace NetHub.Data.SqlServer.Entities;

public class Language : IEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}