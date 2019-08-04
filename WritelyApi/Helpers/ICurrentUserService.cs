using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WritelyApi.Users;

namespace WritelyApi.Helpers
{
    public interface ICurrentUserService
    {
        Task<AppUser> GetCurrentUser(HttpContext httpContext);
    }
}