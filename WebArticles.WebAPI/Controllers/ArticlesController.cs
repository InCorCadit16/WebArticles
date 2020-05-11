using DataModel.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Data.Models;
using WebArticles.WebAPI.Data.Services;

namespace WebArticles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticlesController(ArticleService articleService)
        {
            this._articleService = articleService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatorAnswer<ArticlePreview>>> GetArticlesPreviews([FromQuery] PaginatorQuery queryDto)
        {
            try
            {
                return await _articleService.GetPage(queryDto);
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error quering articles");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleModel>> GetArticle([FromRoute] long id)
        {
            try
            {
                var result = await _articleService.GetArticleById(id);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("user/{id}")]
        public async Task<ActionResult<PaginatorAnswer<ArticlePreview>>> GetArticlesByUserId([FromRoute] long id, [FromQuery] int page)
        {
            try
            {
                return await _articleService.GetPageByUserId(id, page);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error quering articles");
            }
        }

        // Replace with patch
        [HttpPut]
        public async Task<ActionResult<UpdateAnswer>> UpdateArticle([FromBody] ArticleModel articleModel)
        {
            try
            {
                var result = await _articleService.UpdateArticle(articleModel);

                if (result.Succeeded)
                    return Accepted(result);
                else
                    return BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating articles");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateAnswer>> CreateArticle([FromBody] ArticleCreate createArticle)
        {
            var result = await _articleService.CreateArticle(createArticle);
            if (result.Succeeded)
                return Created($"api/articles/{result.Id}", result);
            else
                return BadRequest(result);

        }

        [HttpPut("rating")]
        public async Task<ActionResult<int>> UpdateArticleRating(RatingUpdate ratingUpdate)
        {
            return await _articleService.UpdateRating(ratingUpdate.Id, ratingUpdate.Rating);
        }
    }
}
