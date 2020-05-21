
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Infrastructure.Models;
using WebArticles.WebAPI.Data.Services;

namespace WebArticles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticlesController(ArticleService articleService)
        {
            this._articleService = articleService;
        }

        [AllowAnonymous]
        [HttpPost("paginator")]
        public async Task<ActionResult<PaginatorAnswer<ArticlePreviewDto>>> GetArticlesPreviews([FromBody] PaginatorQuery queryDto)
        {
            return await _articleService.GetArticlesPage(queryDto);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle([FromRoute] long id)
        {
            var result = await _articleService.GetArticleById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("user/{id}")]
        public async Task<ActionResult<PaginatorAnswer<ArticlePreviewDto>>> GetArticlesByUserId([FromRoute] long id, [FromBody] PaginatorQuery paginatorQuery)
        {
            return await _articleService.GetUserArticlesPage(id, paginatorQuery);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle([FromBody] ArticleUpdateDto articleUpdate)
        {
            await _articleService.UpdateArticle(articleUpdate);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleCreateDto createArticle)
        {
            var id = await _articleService.CreateArticle(createArticle);
            
            return Created($"api/articles/{id}", id);
        }

        [HttpPut("rating")]
        public async Task<ActionResult<int>> UpdateArticleRating(RatingUpdateDto ratingUpdate)
        {
            return await _articleService.UpdateRating(ratingUpdate.Id, ratingUpdate.Rating);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] long id)
        {
            await _articleService.DeleteArticle(id);

            return NoContent();
        }
    }
}
