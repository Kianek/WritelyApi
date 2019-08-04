using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace WritelyApi.Users
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserRegistrationDto credentials);

        Task<SignInResult> Login(UserLoginDto credentials);

        Task Logout();

        Task<IdentityResult> UpdateEmail(UserEmailChangeDto dto);

        Task<IdentityResult> UpdatePassword(UserPasswordChangeDto dto);

        Task<IdentityResult> DeleteCurrentUser();

        void InjectHttpContext(HttpContext httpContext);
    }
}