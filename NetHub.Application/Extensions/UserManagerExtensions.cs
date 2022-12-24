using Microsoft.AspNetCore.Identity;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<User> FindByIdAsync(this UserManager<User> userManager, long id) =>
        await userManager.FindByIdAsync(id.ToString());
}