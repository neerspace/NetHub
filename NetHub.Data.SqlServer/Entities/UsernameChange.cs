using Microsoft.EntityFrameworkCore;

namespace NetHub.Data.SqlServer.Entities;

[Owned]
public class UsernameChange
{
	public DateTime? LastTime { get; set; }
	public byte Count { get; set; }
}