using DataModel.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly TopicService _service;

        public TopicsController(TopicService service)
        {
            this._service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Topic[]>> GetTopics()
        {
            try
            {
                return await _service.GetAll();
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
