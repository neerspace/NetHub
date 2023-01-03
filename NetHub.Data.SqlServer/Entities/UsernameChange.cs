using Microsoft.EntityFrameworkCore;

namespace NetHub.Data.SqlServer.Entities;

[Owned]
public class UsernameChange
{
	public DateTimeOffset? LastTime { get; set; }
	public byte Count { get; set; }
}