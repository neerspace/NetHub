using NetHub.Core.Abstractions.Entities;

namespace NetHub.Data.SqlServer.Entities;

public class Language : IEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
}