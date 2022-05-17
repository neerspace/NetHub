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

        private UserProfile? _userProfile;

        public UserProvider(IHttpContextAccessor accessor, UserManager<UserProfile> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public ClaimsPrincipal GetContextUser => _accessor.HttpContext!.User;

        public async Task<long> GetUserId()
        {
            if (await GetUser() is null)
                throw new NotFoundException("User not found");
            return GetContextUser.GetUserId();
        }

        public async Task<UserProfile?> GetUser() =>
            _userProfile ??= await _userManager.FindByIdAsync(GetContextUser.GetUserId());
    }
}