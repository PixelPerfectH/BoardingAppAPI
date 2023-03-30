using BoardingAppAPI.Models.Auth;
using BoardingAppAPI.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.UserName == model.UserName, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            DBUser user = new DBUser(model.UserName, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
