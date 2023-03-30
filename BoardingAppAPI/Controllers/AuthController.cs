using BoardingAppAPI.Helpers;
using BoardingAppAPI.Models.ActionResult;
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
        public async Task<IActionResult> LoginByPasswordAsync([FromBody] LoginByPasswordModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.UserName == model.UserName, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return NotFound();
            }

            var newToken = TokenHelper.GenerateToken();
            user.Tokens.Add(new (newToken));

            return Ok(newToken);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginByTokenAsync([FromBody] LoginByTokenModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Tokens.FirstOrDefault(token => token.Token == model.Token) != null, cancellationToken);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(new UserResult(user));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            var newToken = TokenHelper.GenerateToken();
            var user = new DBUser(model.UserName, model.Password, newToken);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(newToken);
        }
    }
}
