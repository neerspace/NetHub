using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Services;

public interface IUserProvider
{
    long UserId { get; }
    string UserName { get; }
    long? TryGetUserId();
    Task<AppUser> GetUserAsync();
}