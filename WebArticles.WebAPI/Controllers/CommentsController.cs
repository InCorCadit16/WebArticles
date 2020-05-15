using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            this._commentService = commentService;
        }

        [HttpGet("{articleId}")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatorAnswer<CommentModel>>> GetPage([FromRoute] long articleId, [FromQuery] PaginatorQuery query)
        {
            try
            {
                return await _commentService.GetPage(articleId, query);
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error loading articles");
            }
        }

        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatorAnswer<CommentModel>>> GetPageByUserId([FromRoute] long userId, [FromQuery] PaginatorQuery query)
        {
            try
            {
                return await _commentService.GetPageByUserId(userId, query);
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error loading articles");
            }
        }

        // Replace with patch
        [HttpPut]
        public async Task<ActionResult<UpdateAnswer>> UpdateComment([FromBody] CommentUpdate commentUpdate)
        {
            return await _commentService.UpdateComment(commentUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UpdateAnswer>> UpdateComment([FromRoute] long id)
        {
            var result = await _commentService.DeleteComment(id);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateAnswer>> CreateComment([FromBody] CommentCreate commentCreate)
        {
            var result = await _commentService.CreateComment(commentCreate);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut("rating")]
        public async Task<ActionResult<int>> UpdateCommentRating(RatingUpdate ratingUpdate)
        {
            return await _commentService.UpdateRating(ratingUpdate.Id, ratingUpdate.Rating);
        }
    }
}
