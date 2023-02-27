using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

[Table($"{nameof(Resource)}s")]
public class Resource : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Filename { get; set; } = default!;
    public string Mimetype { get; set; } = default!;
    public byte[] Bytes { get; set; } = default!;
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
}