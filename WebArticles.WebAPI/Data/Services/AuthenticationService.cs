using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Infrastructure;

namespace WebArticles.WebAPI.Data.Services
{
    public class AuthenticationService
    {
        private readonly AuthOptions _authenticationOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationService(IOptions<AuthOptions> authenticationOptions,
                                        SignInManager<User> signInManager,
                                        UserManager<User> userManager,
                                        UserService userService,
                                        IMapper mapper)
        {
            this._authenticationOptions = authenticationOptions.Value;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._userService = userService;
            this._mapper = mapper;
        }

        public async Task<UserLoginAnswer> Login(UserLoginQuery userLoginDto)
        {
            var checkPasswordResult = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, false, false);

            if (checkPasswordResult.Succeeded)
            {
                var user = await _userService.GetUserByUserName(userLoginDto.UserName);
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, roles.Aggregate((r, i) => i += r + ","))
                };

                var signingCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                var jwtToken = new JwtSecurityToken(
                    issuer: _authenticationOptions.Issuer,
                    audience: _authenticationOptions.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signingCredentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var encodedToken = tokenHandler.WriteToken(jwtToken);

                return new UserLoginAnswer() { EncodedToken = encodedToken, UserId = user.Id };
            }
            return new UserLoginAnswer() { ErrorMessage = "Wrong username or password" };
        }

        public async Task<UserRegisterAnswer> Register(UserRegisterQuery userRegisterDto)
        {
            try
            {
                var user = _mapper.Map<User>(userRegisterDto);
                var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

                if (result.Succeeded)
                {
                    _userService.CreateWriterAndReviewer(user);
                    await _userManager.AddToRoleAsync(user, "User");
                    return new UserRegisterAnswer { User = user };
                }

                return new UserRegisterAnswer
                { ErrorMessage = result.Errors.Select(e => e.Description).Aggregate((d, res) => res += d + "\n") };
            }
            catch (DbUpdateException e)
            {
                return new UserRegisterAnswer { ErrorMessage = $"Email \'{userRegisterDto.Email}\' is already taken" };
            } catch (Exception e)
            {
                return new UserRegisterAnswer { ErrorMessage = "Failed to register. Server error. Try later" };
            }
        }
    }
}
