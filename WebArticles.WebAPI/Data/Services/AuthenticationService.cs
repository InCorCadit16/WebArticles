using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.AspNetCore.Authentication;
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
using System.Security.Policy;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Repositories.Implementations;
using WebArticles.WebAPI.Infrastructure;
using WebArticles.WebAPI.Infrastructure.Exceptions;

namespace WebArticles.WebAPI.Data.Services
{
    public class AuthenticationService
    {
        private readonly AuthOptions _authenticationOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public AuthenticationService(IOptions<AuthOptions> authenticationOptions,
                                        SignInManager<User> signInManager,
                                        UserManager<User> userManager,
                                        UserService userService,
                                        IMapper mapper,
                                        UserRepository repository)
        {
            this._authenticationOptions = authenticationOptions.Value;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._userService = userService;
            this._mapper = mapper;
            this._repository = repository;
        }

        public async Task<UserLoginAnswerDto> Login(UserLoginQueryDto userLoginDto)
        {
            var checkPasswordResult = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, false, false);

            if (!checkPasswordResult.Succeeded)
            { 
                throw new FormInvalidException($"", "Wrong username or password", StatusCodes.Status401Unauthorized);
            }

            var user = await _userService.GetUserByUserName(userLoginDto.UserName);
            string token = await GenerateToken(user);

            return new UserLoginAnswerDto() { EncodedToken = token, UserId = user.Id };
        }

        public AuthenticationProperties ConfigureProperties(IUrlHelper url, string redirectMethod)
        {
            
            return _signInManager.ConfigureExternalAuthenticationProperties("Google", url.Action(redirectMethod));
        }

        public async Task HandleExternalLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (!result.Succeeded)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var newUser = new User
                {
                    UserName = email,
                    Email = email
                };
                var createResult = await _userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                {
                    throw new FormInvalidException("", createResult.Errors.Select(e => e.Description).Aggregate((d, res) => res += d + "\n"));
                }

                await _userService.CreateWriterAndReviewer(newUser);
                await _userManager.AddLoginAsync(newUser, info);
                await SignInExternalUser(newUser, info);

            }
        }

        private async Task SignInExternalUser(User user, ExternalLoginInfo info)
        {
            var userClaims = info.Principal.Claims.Append(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()))
                                .Append(new Claim(ClaimTypes.Name, user.UserName))
                                .Append(new Claim(ClaimTypes.Role, "User"));

            await _userManager.AddClaimsAsync(user, userClaims);
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<UserRegisterAnswerDto> Register(UserRegisterQueryDto userRegisterDto)
        {
            if ((await _repository.GetUserByEmail(userRegisterDto.Email)) != null)
            {
                throw new FormInvalidException("", $"Email \'{userRegisterDto.Email}\' is already taken");
            }

            var user = _mapper.Map<User>(userRegisterDto);
            var result = await RegisterNewUser(user, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                throw new FormInvalidException("", result.Errors.Select(e => e.Description).Aggregate((d, res) => res += d + "\n"));
            }

            return new UserRegisterAnswerDto { User = user };
        }

        private async Task<string> GenerateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

            roles.ToList().ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));

            var signingCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: _authenticationOptions.Issuer,
                audience: _authenticationOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwtToken);
        }

        private async Task<IdentityResult> RegisterNewUser(User user, string password = null)
        {
            IdentityResult result;
            if (password == null)
                result = await _userManager.CreateAsync(user);
            else
                result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userService.CreateWriterAndReviewer(user);
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }
    }
}
