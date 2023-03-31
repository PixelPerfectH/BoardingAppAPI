using BoardingAppAPI.Models.Auth;
using BoardingAppAPI.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly BoardingAppContext _context;

        public AuthController(BoardingAppContext context, ILogger<AuthController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> LoginByPasswordAsync([FromBody] LoginByPasswordModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.UserName == model.UserName, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.HashedPassword))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            var user = new DBUser()
            {
                UserName = model.UserName,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
