using System.Security.Claims;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Services;

public interface IUserProvider
{
    ClaimsPrincipal GetContextUser { get; }
    Task<long> GetUserId();
    Task<UserProfile?> GetUser();
}