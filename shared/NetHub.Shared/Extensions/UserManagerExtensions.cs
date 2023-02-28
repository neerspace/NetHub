using Microsoft.AspNetCore.Identity;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Shared.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser?> GetByIdAsync(this UserManager<AppUser> userManager, long id) =>
        await userManager.FindByIdAsync(id.ToString());
}