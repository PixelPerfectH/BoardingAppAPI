using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetList(string token)
        {
            return Ok();
        }
    }
}
