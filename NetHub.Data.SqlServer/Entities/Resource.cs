using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

public class Resource : IEntity
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Filename { get; set; } = default!;
	public string Mimetype { get; set; } = default!;
	public byte[] Bytes { get; set; } = default!;
	public DateTime Created { get; set; } = DateTime.UtcNow;
}