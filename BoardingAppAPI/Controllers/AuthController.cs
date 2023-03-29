using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BoardingAppContext _context;

        public AuthController(BoardingAppContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return Ok();
        }
    }
}
