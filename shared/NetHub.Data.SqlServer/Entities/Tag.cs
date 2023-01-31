using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

[Table($"{nameof(Tag)}s")]
public record Tag : IEntity
{
	public long Id { get; set; }
	public string Name { get; set; } = default!;
}