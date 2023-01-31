using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Shared.Services;

public interface IUserProvider
{
    long UserId { get; }
    string UserName { get; }
    long? TryGetUserId();
    Task<AppUser> GetUserAsync();
}