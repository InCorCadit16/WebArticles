using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Infrastructure.Models;
using WebArticles.WebAPI.Data.Services;
using System.Security.Claims;

namespace WebArticles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            this._userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _userService.GetUserModelById(id);
            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userModel)
        {
            await _userService.UpdateUser(userModel);
            return NoContent();

        }

        [HttpGet("my/article/{articleId}")]
        public async Task<ActionResult<long>> GetUserArticlesId(long articleId)
        {
            var result = await _userService.GetUserArticlesId(articleId);
            var loggedUserId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (result != null && loggedUserId == result.Id)
                return Ok(result.Id);
            else
                return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("{id}/pick")]
        public async Task<IActionResult> GetProfilePickLink(long id)
        {
            var result = await _userService.GetProfilePickLink(id);
            if (result != null)
                return Ok( new { profilePick = result });
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PaginatorAnswer<UserRowDto>>> GetUserRowsPage([FromBody] PaginatorQuery query) {
            return Ok(await _userService.GetPage(query));
        }
    }
}
