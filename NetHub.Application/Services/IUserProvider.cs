using System.Security.Claims;

namespace NetHub.Application.Services;

public interface IUserProvider
{
    ClaimsPrincipal GetContextUser { get; }
    Task<long> GetUserId();
}