using DataModel.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Data.Services;
using WebArticles.WebAPI.Infrastructure;

namespace WebArticles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQuery userLoginDto)
        {
            var result = await _authenticationService.Login(userLoginDto);
            if (result.ErrorMessage == null)
                return Ok(result);
            else
                return Unauthorized(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterQuery userRegisterDto)
        {
            var result = await _authenticationService.Register(userRegisterDto);
            if (result.ErrorMessage == null)
                return Created($"api/users/{result.User.Id}", result);
            else
                return BadRequest(result);
           
        }
    }
}
