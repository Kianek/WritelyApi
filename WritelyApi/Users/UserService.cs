using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace WritelyApi.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _usrManager;
        private readonly SignInManager<AppUser> _signInManager;
        private HttpContext _httpContext;

        public UserService(UserManager<AppUser> usrManager, SignInManager<AppUser> signInManager)
        {
            _usrManager = usrManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(UserRegistrationDto creds)
        {
            var newUser = new AppUser { UserName = creds.UserName, Email = creds.Email };
            return await _usrManager.CreateAsync(newUser, creds.Password);
        }

        public async Task<SignInResult> Login(UserLoginDto creds)
        {
            var user = await _usrManager.FindByEmailAsync(creds.Email);
            return await _signInManager.PasswordSignInAsync(user, creds.Password, false, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateEmail(UserEmailChangeDto dto)
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return IdentityResult.Failed();
            }

            user.Email = dto.Email;
            user.NormalizedEmail = _usrManager.NormalizeKey(dto.Email);
            return await _usrManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdatePassword(UserPasswordChangeDto dto)
        {
            var user = await GetCurrentUser();
            if (user != null)
            {
                var result = await _usrManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            }

            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteCurrentUser()
        {
            return await _usrManager.DeleteAsync(await GetCurrentUser());
        }


        // Inject the controller's HttpContext object in order to have access to
        // the claims principal object. This, in turn, allows loading of the
        // current user.
        public void InjectHttpContext(HttpContext httpContext) => _httpContext = httpContext;

        private async Task<AppUser> GetCurrentUser()
        {
            return await _usrManager.GetUserAsync(_httpContext.User);
        }
    }
}