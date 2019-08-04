using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WritelyApi.Users;

namespace WritelyApi.Helpers
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public CurrentUserService(UserManager<AppUser> manager) => _userManager = manager;

        public async Task<AppUser> GetCurrentUser(HttpContext httpContext)
        {
            return await _userManager.GetUserAsync(httpContext.User);
        }
    }
}