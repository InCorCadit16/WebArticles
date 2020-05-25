using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Services;

namespace WebArticles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthenticationController : BaseController
    {
        private readonly Data.Services.AuthenticationService _authenticationService;

        public AuthenticationController(Data.Services.AuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQueryDto userLoginDto)
        {
            var result = await _authenticationService.Login(userLoginDto);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]    
        public async Task<IActionResult> Register([FromBody] UserRegisterQueryDto userRegisterDto)
        {
            var result = await _authenticationService.Register(userRegisterDto);
            return Created($"api/users/{result.User.Id}", result);
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> SignInWithGoogle()
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value))
            {
                var token = value.ToString().Substring(7);
                var result = await _authenticationService.LoginWithGoogle(token);
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return Ok();
        }
    }
}
