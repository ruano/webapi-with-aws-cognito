using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICongitoDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "Writer")]
        public IActionResult Add()
        {
            return NoContent();
        }
    }
}