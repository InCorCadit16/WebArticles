using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Services;
using WebArticles.WebAPI.Infrastructure.Models;

namespace WebArticles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : BaseController
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            this._commentService = commentService;
        }

        [HttpPost("article/{articleId}")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatorAnswer<CommentDto>>> GetArtcleCommentsPage([FromRoute] long articleId, [FromBody] PaginatorQuery query)
        {
            return await _commentService.GetArticleCommentsPage(articleId, query);
        }

        [HttpPost("user/{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatorAnswer<CommentDto>>> GetUserCommentsPage([FromRoute] long userId, [FromBody] PaginatorQuery query)
        {
            return await _commentService.GetUserCommentsPage(userId, query);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentUpdateDto commentUpdate)
        {
           await _commentService.UpdateComment(commentUpdate);
           return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(long id)
        {
            await _commentService.DeleteComment(id);
            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateDto commentCreate)
        {
            var id = await _commentService.CreateComment(commentCreate);
            return Ok(id);
        }

        [HttpGet("{commentId}/rating")]
        public async Task<ActionResult<int>> GetUserArticleMark(long commentId)
        {
            long userId = long.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var mark = await _commentService.GetUserCommentMark(userId, commentId);
            return Ok(mark);
        }

        [HttpPut("rating")]
        public async Task<ActionResult<int>> UpdateCommentRating(RatingUpdateDto ratingUpdate)
        {
            long userId = long.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return await _commentService.UpdateRating(userId, ratingUpdate.Id, ratingUpdate.NewMark);
        }
    }
}
