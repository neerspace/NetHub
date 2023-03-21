using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

public class Feedback: IEntity<long>
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Message { get; set; } = default!;
}