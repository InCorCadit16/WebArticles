using AutoMapper;
using WebArticles.DataModel.Entities;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<UserLoginAnswerDto> LoginWithGoogle(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken decodedToken = null;
            try
            {
                decodedToken = handler.ReadJwtToken(token);
            } catch
            {
                throw new FormInvalidException("", "Cannot read authentication token");
            }
            

            var email = decodedToken.Claims.First(c => c.Type == "email").Value;
            var user = await _repository.GetUserByEmail(email);
            if (user != null)
            {
                if (user.ExternalProvider)
                {
                    await _signInManager.SignInAsync(user, false);
                    string encodedToken = await GenerateToken(user);
                    return new UserLoginAnswerDto { EncodedToken = encodedToken, UserId = user.Id };
                }
                else
                {
                    throw new FormInvalidException("", "This email have been already taken by a password based user. Please log in using your password.", StatusCodes.Status401Unauthorized);
                }
            }
            else
            {
                string userName = decodedToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                if ((await _repository.GetUserByUserName(userName)) != null)
                    userName = await CreateUniqueUserName(userName);

                user = new User
                {
                    Email = email,
                    FirstName = decodedToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value,
                    LastName = decodedToken.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value,
                    ProfilePickLink = decodedToken.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                    ExternalProvider = true,
                    UserName = userName
                };

                var result = await RegisterNewUser(user);

                if (result.Succeeded)
                {
                    var encodedToken = await GenerateToken(user);
                    return new UserLoginAnswerDto { EncodedToken = encodedToken, UserId = user.Id };
                } else
                {
                    throw new FormInvalidException(result.Errors.Select(err => err.Description).Aggregate((desc, msg) => msg += desc + "\n"), "An unexpected error occured. Try later", StatusCodes.Status500InternalServerError);
                }
            }
        }


        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
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

        private async Task<IdentityResult> RegisterNewUser(User user, string password = null)
        {
            IdentityResult result;
            if (password == null)
                result = await _userManager.CreateAsync(user);
            else
                result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _repository.SaveAllChanges();
                await _userService.CreateWriterAndReviewer(user);
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        private async Task<string> GenerateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.AuthenticationMethod, user.ExternalProvider? "external": "internal")
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

        private async Task<string> CreateUniqueUserName(string userName)
        {
            int i = 0;
            while ((await _repository.GetUserByUserName($"{userName} {++i}")) != null) ;

            return userName + i;
        }
    }
}

