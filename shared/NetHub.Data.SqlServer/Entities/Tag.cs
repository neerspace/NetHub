using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

public record Tag : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
}