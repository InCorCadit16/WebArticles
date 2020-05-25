using WebArticles.DataModel.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Services;
using WebArticles.WebAPI.Controllers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : BaseController
    {
        private readonly TopicService _topicService;

        public TopicsController(TopicService service)
        {
            this._topicService = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<TopicDto[]>> GetTopics()
        {
            return await _topicService.GetAll();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTopic([FromBody] TopicDto topic) 
        {
            var newTopic = await _topicService.CreateTopic(topic.TopicName);
            if (newTopic != null)
                return Ok(newTopic);
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTopic(long id)
        {
            await _topicService.DeleteTopic(id);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TopicDto>> UpdateTopic([FromBody] TopicDto topic)
        {
            var updatedTopic = await _topicService.UpdateTopic(topic);
            if (updatedTopic != null)
                return Ok(updatedTopic);
            else
                return BadRequest();
        }
    }
}
