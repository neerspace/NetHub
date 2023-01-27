using Microsoft.AspNetCore.Identity;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser?> FindByIdAsync(this UserManager<AppUser> userManager, long id) =>
        await userManager.FindByIdAsync(id.ToString());
}