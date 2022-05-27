using System.Security.Claims;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Services;

public interface IUserProvider
{
    ClaimsPrincipal User { get; }
    long GetUserId();
    UserProfile GetUser();
}