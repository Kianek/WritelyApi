using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WritelyApi.Helpers;

namespace WritelyApi.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(
            IUserService service)
        {
            _service = service;
        }

        // POST - api/users/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto creds)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Register(creds);
                if (result.Succeeded)
                {
                    return Ok(MessageGenerator.Generate(UserResponses.AccountCreated));
                }
                else
                {
                    return BadRequest(MessageGenerator.GenerateErrors(result));
                }
            }

            return BadRequest(ModelState);
        }

        // POST - api/users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto creds)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(creds);
                if (result.Succeeded)
                {
                    return Ok(MessageGenerator.Generate(UserResponses.LoginSucceeded));
                }
                else
                {
                    return BadRequest(MessageGenerator.Generate(UserResponses.LoginFailed));
                }
            }

            return BadRequest(ModelState);
        }

        // GET - api/users/logout
        [HttpGet("logout")]
        public async Task<OkObjectResult> Logout()
        {
            await _service.Logout();
            return Ok(MessageGenerator.Generate(UserResponses.LoggedOut));
        }


        // PUT - api/users/update-email
        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateEmail([FromBody] UserEmailChangeDto dto)
        {
            if (ModelState.IsValid)
            {
                _service.InjectHttpContext(HttpContext);
                var result = await _service.UpdateEmail(dto);
                if (result.Succeeded)
                {
                    return Ok(MessageGenerator.Generate(UserResponses.EmailUpdated));
                }
                else
                {
                    return BadRequest(MessageGenerator.GenerateErrors(result));
                }
            }

            return BadRequest(ModelState);
        }

        // PUT - api/users/update-password
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UserPasswordChangeDto dto)
        {
            if (ModelState.IsValid)
            {
                _service.InjectHttpContext(HttpContext);
                var result = await _service.UpdatePassword(dto);
                if (result.Succeeded)
                {
                    return Ok(MessageGenerator.Generate(UserResponses.PasswordUpdated));
                }
                else
                {
                    return BadRequest(MessageGenerator.GenerateErrors(result));
                }
            }

            return BadRequest(ModelState);
        }

        // DELETE - api/users/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAccount()
        {
            // Inject the HttpContext so that the UserService can access the current user
            _service.InjectHttpContext(HttpContext);
            var result = await _service.DeleteCurrentUser();
            if (result.Succeeded)
            {
                return Ok(MessageGenerator.Generate(UserResponses.AccountDeleted));
            }

            return BadRequest(MessageGenerator.GenerateErrors(result));
        }
    }
}