using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NetHub.Application.Extensions;
using NetHub.Application.Services;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Infrastructure.Services
{
    [Inject]
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<UserProfile> _userManager;

        public UserProvider(IHttpContextAccessor accessor, UserManager<UserProfile> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public ClaimsPrincipal GetContextUser => _accessor.HttpContext!.User;

        public async Task<long> GetUserId()
        {
            var userId = GetContextUser.GetUserId();
            if (await _userManager.FindByIdAsync(userId.ToString()) is null)
                throw new NotFoundException("User not found");
            return userId;
        }
    }
}