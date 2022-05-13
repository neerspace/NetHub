using Microsoft.AspNetCore.Identity;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<UserProfile> FindByIdAsync(this UserManager<UserProfile> userManager, long id) =>
        await userManager.FindByIdAsync(id.ToString());
}