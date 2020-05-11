using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Data.Models;
using WebArticles.WebAPI.Data.Services;

namespace WebArticles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;

        public UsersController(UserService userService, UserManager<User> userManager)
        {
            this._userService = userService;
            this._userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {
            var user = await _userService.GetUserModelById(id);
            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult<UpdateAnswer>> UpdateUser([FromBody] UserModel userModel)
        {
            var result = await _userService.UpdateUser(userModel);
            if (result.Succeeded)
                return Accepted(result);
            else
                return BadRequest(result);

        }

        [HttpGet("article/{articleId}")]
        public async Task<ActionResult<long>> GetUserIdByArticleId([FromRoute] long articleId)
        {
            var result = await _userService.GetUserIdByArticleId(articleId);
            if (result != null)
                return Ok(result.Id);
            else
                return NotFound();
        }

        [HttpGet("{id}/pick")]
        public async Task<IActionResult> GetProfilePickLink([FromRoute] long id)
        {
            var result = await _userService.GetProfilePickLink(id);
            if (result != null)
                return Ok( new { profilePick = result });
            else
                return NotFound();
        }
    }
}
