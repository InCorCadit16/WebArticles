using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Services;

namespace WebArticles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly Data.Services.AuthenticationService _authenticationService;

        public AuthenticationController(Data.Services.AuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQueryDto userLoginDto)
        {
            var result = await _authenticationService.Login(userLoginDto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterQueryDto userRegisterDto)
        {
            var result = await _authenticationService.Register(userRegisterDto);
            return Created($"api/users/{result.User.Id}", result);
        }

        
        [HttpPost("google")]
        public IActionResult SignInWithGoogle()
        {
            var authenticationProperties = _authenticationService.ConfigureProperties(Url, nameof(HandleExternalLogin));
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Response.Headers.Add("Access-Control-Max-Age", "1000");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "X-Requested-With, Content-Type, Origin, Authorization");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "PUT, GET, POST, DELETE, OPTIONS");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            HttpContext.Response.Headers.Add("Content-Length", "0");
            return Challenge(authenticationProperties, "Google");
        }

        public async Task<IActionResult> HandleExternalLogin()
        {
            await _authenticationService.HandleExternalLogin();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return Redirect("http://localhost:4200/main");
        }
    }
}
