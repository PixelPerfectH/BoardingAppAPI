using BoardingAppAPI.Models;
using BoardingAppAPI.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly BoardingAppContext _context;

        public TaskController(BoardingAppContext context, ILogger<TaskController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync(string userName, string taskName, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);

            if (user is null)
            {
                return NotFound();
            }



            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CompleteAsync([FromBody] TaskModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.UserName == model.userName, cancellationToken);

            if (user is null)
            {
                return NotFound();
            }



            for (int i = 0; i < user.Tasks.Count; i++)
            {
                if (user.Tasks[i].Name == model.taskName)
                {
                    user.Tasks[i].IsActive = false;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }

            return NotFound();
        }
    }
}
