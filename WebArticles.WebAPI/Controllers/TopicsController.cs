using DataModel.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Data.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TopicsController : ControllerBase
    {
        private readonly TopicService _topicService;

        public TopicsController(TopicService service)
        {
            this._topicService = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Topic[]>> GetTopics()
        {
            return await _topicService.GetAll();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<CreateAnswer> CreateTopic([FromBody] Topic topic) 
        {
            var result = _topicService.CreateTopic(topic.TopicName);

            if (result.Succeeded)
                return Created("", result);
            else
                return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UpdateAnswer>> DeleteTopic([FromRoute] long id)
        {
            var result = await _topicService.DeleteTopic(id);
            if (result.Succeeded)
                return Accepted(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult<UpdateAnswer> UpdateTopic([FromBody] Topic topic)
        {
            var result = _topicService.UpdateTopic(topic);
            if (result.Succeeded)
                return Accepted(result);
            else
                return BadRequest(result);
        }
    }
}
