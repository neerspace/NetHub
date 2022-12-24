using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

[Table($"{nameof(Language)}s")]
public sealed class Language : IEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}