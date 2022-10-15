using System.Security.Claims;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Interfaces;

public interface IUserProvider
{
    ClaimsPrincipal User { get; }
    long GetUserId();
    long? TryGetUserId();
    Task<User> GetUser();
}